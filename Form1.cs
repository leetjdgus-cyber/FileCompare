using System.IO;
using System.Globalization;
using System.Drawing;
using System.Linq;
using System.Collections.Generic;

namespace FileCompare
{
    public partial class Form1 : Form
    {
        private static readonly Color[] RowPalette = new Color[]
        {
            Color.White,
            Color.Lavender,
            Color.LemonChiffon,
            Color.MistyRose,
            Color.Honeydew,
            Color.LavenderBlush,
            Color.LightCyan,
            Color.Beige
        };

        public Form1()
        {
            InitializeComponent();
        }

        private void CompareAndColorMatches(ListView left, ListView right, string leftDir, string rightDir)
        {
            if (left == null || right == null) return;

            // build set of names on right
            var rightIndexByName = new Dictionary<string, int>(StringComparer.CurrentCultureIgnoreCase);
            for (int j = 0; j < right.Items.Count; j++)
            {
                rightIndexByName[right.Items[j].Text] = j;
            }

            for (int i = 0; i < left.Items.Count; i++)
            {
                var lit = left.Items[i];
                if (rightIndexByName.TryGetValue(lit.Text, out var rIndex))
                {
                    var color = RowPalette[(i) % RowPalette.Length];
                    lit.BackColor = color;
                    right.Items[rIndex].BackColor = color;

                    // compare modified dates if both exist
                    var leftPath = Path.Combine(leftDir ?? string.Empty, lit.Text);
                    var rightPath = Path.Combine(rightDir ?? string.Empty, right.Items[rIndex].Text);

                    try
                    {
                        DateTime? leftTime = null, rightTime = null;
                        if (File.Exists(leftPath)) leftTime = File.GetLastWriteTime(leftPath);
                        else if (Directory.Exists(leftPath)) leftTime = Directory.GetLastWriteTime(leftPath);

                        if (File.Exists(rightPath)) rightTime = File.GetLastWriteTime(rightPath);
                        else if (Directory.Exists(rightPath)) rightTime = Directory.GetLastWriteTime(rightPath);

                        if (leftTime.HasValue && rightTime.HasValue)
                        {
                            if (leftTime.Value != rightTime.Value)
                            {
                                // newer -> red, older -> black for date subitem
                                if (leftTime.Value > rightTime.Value)
                                {
                                    lit.SubItems[2].ForeColor = Color.Red;
                                    right.Items[rIndex].SubItems[2].ForeColor = Color.Black;
                                }
                                else
                                {
                                    lit.SubItems[2].ForeColor = Color.Black;
                                    right.Items[rIndex].SubItems[2].ForeColor = Color.Red;
                                }
                            }
                            else
                            {
                                // same time -> use default dim gray
                                lit.SubItems[2].ForeColor = Color.DimGray;
                                right.Items[rIndex].SubItems[2].ForeColor = Color.DimGray;
                            }
                        }
                    }
                    catch
                    {
                        // ignore
                    }
                }
            }
        }

        private void listView1_ItemActivate(object sender, EventArgs e)
        {
            ShowSubItemsForSelected(listView1, txtLeftDir.Text);
        }

        private void lvwRightDir_ItemActivate(object sender, EventArgs e)
        {
            ShowSubItemsForSelected(lvwRightDir, txtRightDir.Text);
        }

        private void ShowSubItemsForSelected(ListView lv, string baseDir)
        {
            if (lv.SelectedItems.Count == 0) return;
            var sel = lv.SelectedItems[0];
            var name = sel.Text;
            var path = Path.Combine(baseDir, name);

            if (Directory.Exists(path))
            {
                // list top-level entries in a dialog
                try
                {
                    var entries = Directory.EnumerateFileSystemEntries(path)
                        .Select(p => Path.GetFileName(p) + (Directory.Exists(p) ? "\t<DIR>" : "\tF"))
                        .ToArray();

                    MessageBox.Show(string.Join("\n", entries), $"{name} 내용", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "오류", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void splitContainer1_Panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void panel3_Paint(object sender, PaintEventArgs e)
        {

        }

        private void splitContainer1_SplitterMoved(object sender, SplitterEventArgs e)
        {

        }

        private void panel5_Paint(object sender, PaintEventArgs e)
        {

        }

        private void btnCopyFromLeft_Click(object sender, EventArgs e)
        {
            CopySelectedFiles(listView1, txtLeftDir.Text, txtRightDir.Text, lvwRightDir);
        }

        private void btnLeftDir_Click(object sender, EventArgs e)
        {
            using (var dlg = new FolderBrowserDialog())
            {
                dlg.Description = "폴더를 선택하세요.";

                // 현재 텍스트박스에 있는 경로를 초기 선택 폴더로 설정
                if (!string.IsNullOrWhiteSpace(txtRightDir.Text) &&
                                Directory.Exists(txtRightDir.Text))
                {
                    dlg.SelectedPath = txtRightDir.Text;
                }

                if (dlg.ShowDialog() == DialogResult.OK)
                {
                    txtLeftDir.Text = dlg.SelectedPath;
                    LoadDirectoryToListView(txtLeftDir.Text, listView1);
                    // color matches if right side already loaded
                    CompareAndColorMatches(listView1, lvwRightDir, txtLeftDir.Text, txtRightDir.Text);
                }


            }
        }

        private void lvwLeftDir_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void LoadDirectoryToListView(string path, ListView lv)
        {
            if (!Directory.Exists(path)) return;
            lv.BeginUpdate();
            lv.Items.Clear();

            try
            {
                var dirInfo = new DirectoryInfo(path);
                var dirs = dirInfo.GetDirectories();
                var files = dirInfo.GetFiles();

                // show directories first, then files
                // directories will be displayed as items with size column as "<DIR>"
                foreach (var d in dirs)
                {
                    var item = new ListViewItem(d.Name);
                    item.SubItems.Add("<DIR>");
                    item.SubItems.Add(d.LastWriteTime.ToString("yyyy-MM-dd HH:mm", CultureInfo.CurrentCulture));

                    // color by current visual index so left/right align
                    var indexForColor = lv.Items.Count;
                    item.BackColor = RowPalette[indexForColor % RowPalette.Length];
                    item.SubItems[0].ForeColor = Color.DarkMagenta;
                    item.SubItems[1].ForeColor = Color.DarkGray;
                    item.SubItems[2].ForeColor = Color.DimGray;

                    lv.Items.Add(item);
                }

                // then files

                // ensure columns alignment
                if (lv.Columns.Count >= 3)
                {
                    lv.Columns[0].TextAlign = HorizontalAlignment.Left;
                    lv.Columns[1].TextAlign = HorizontalAlignment.Right;
                    lv.Columns[2].TextAlign = HorizontalAlignment.Right;
                }

                foreach (var f in files)
                {
                    var item = new ListViewItem(f.Name);
                    item.SubItems.Add(FormatFileSize(f.Length));
                    item.SubItems.Add(f.LastWriteTime.ToString("yyyy-MM-dd HH:mm", CultureInfo.CurrentCulture));

                    // unified coloring: use a palette so rows show a variety of colors consistently
                    var indexForColor = lv.Items.Count;
                    item.BackColor = RowPalette[indexForColor % RowPalette.Length];

                    // varied subitem colors: name, size, date
                    item.SubItems[0].ForeColor = Color.FromArgb(30, 60, 120); // deep blue-ish for name

                    // size-based coloring (small -> green, medium -> orange, large -> red)
                    if (f.Length > 50_000_000) item.SubItems[1].ForeColor = Color.DarkRed;
                    else if (f.Length > 5_000_000) item.SubItems[1].ForeColor = Color.OrangeRed;
                    else if (f.Length > 500_000) item.SubItems[1].ForeColor = Color.DarkOrange;
                    else if (f.Length > 100_000) item.SubItems[1].ForeColor = Color.SeaGreen;
                    else item.SubItems[1].ForeColor = Color.ForestGreen;

                    // date coloring: very recent -> teal, recent -> cadetblue, older -> dimgray
                    var age = DateTime.Now - f.LastWriteTime;
                    if (age.TotalDays < 1) item.SubItems[2].ForeColor = Color.Teal;
                    else if (age.TotalDays < 7) item.SubItems[2].ForeColor = Color.CadetBlue;
                    else item.SubItems[2].ForeColor = Color.DimGray;

                    lv.Items.Add(item);
                }

                // adjust column widths
                if (lv.Columns.Count >= 3)
                {
                    lv.Columns[1].Width = 100;
                    lv.Columns[2].Width = 140;
                    lv.Columns[0].Width = Math.Max(100, lv.Width - (lv.Columns[1].Width + lv.Columns[2].Width + 20));
                }
            }
            catch
            {
                // ignore for now
            }
            finally
            {
                lv.EndUpdate();
            }
        }

        private string FormatFileSize(long bytes)
        {
            const long KB = 1024;
            const long MB = KB * 1024;
            const long GB = MB * 1024;

            if (bytes >= GB) return (bytes / (double)GB).ToString("0.##") + " GB";
            if (bytes >= MB) return (bytes / (double)MB).ToString("0.##") + " MB";
            if (bytes >= KB) return (bytes / (double)KB).ToString("0.##") + " KB";
            return bytes + " B";
        }

        private void panel5_Paint_1(object sender, PaintEventArgs e)
        {

        }

        private void btnCopyFromRight_Click(object sender, EventArgs e)
        {
            CopySelectedFiles(lvwRightDir, txtRightDir.Text, txtLeftDir.Text, listView1);
        }

        private void CopySelectedFiles(ListView sourceListView, string sourceDir, string destDir, ListView destListView)
        {
            if (string.IsNullOrWhiteSpace(sourceDir) || !Directory.Exists(sourceDir))
            {
                MessageBox.Show("원본 폴더 경로가 유효하지 않습니다.", "오류", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            if (string.IsNullOrWhiteSpace(destDir) || !Directory.Exists(destDir))
            {
                MessageBox.Show("대상 폴더 경로가 유효하지 않습니다.", "오류", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (sourceListView.SelectedItems.Count == 0)
            {
                MessageBox.Show("복사할 파일을 선택하세요.", "정보", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            int success = 0;
            var errors = new List<string>();

            foreach (ListViewItem item in sourceListView.SelectedItems)
            {
                var fileName = item.Text;
                var srcPath = Path.Combine(sourceDir, fileName);
                var dstPath = Path.Combine(destDir, fileName);

                try
                {
                    // if source is a directory, copy recursively
                    if (Directory.Exists(srcPath))
                    {
                        // confirm overwrite if destination exists
                        if (Directory.Exists(dstPath))
                        {
                            var srcTime = Directory.GetLastWriteTime(srcPath);
                            var dstTime = Directory.GetLastWriteTime(dstPath);
                            var prompt = $"폴더 '{fileName}'이(가) 대상 폴더에 이미 존재합니다.\n\n" +
                                         $"보내는 쪽 수정일: {srcTime:yyyy-MM-dd HH:mm}\n" +
                                         $"대상 쪽 수정일: {dstTime:yyyy-MM-dd HH:mm}\n\n" +
                                         "덮어쓰시겠습니까? (예: 덮어쓰기, 아니요: 건너뜀)";

                            var ask = MessageBox.Show(prompt, "폴더 덮어쓰기 확인", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                            if (ask == DialogResult.No) continue; // skip directory
                        }

                        try
                        {
                            var copiedFiles = DirectoryCopy(srcPath, dstPath, true);
                            success += copiedFiles;
                        }
                        catch (Exception ex)
                        {
                            errors.Add($"{fileName}: {ex.Message}");
                        }

                        continue;
                    }

                    // handle file copy
                    if (File.Exists(srcPath))
                    {
                        if (File.Exists(dstPath))
                        {
                            var srcTime = File.GetLastWriteTime(srcPath);
                            var dstTime = File.GetLastWriteTime(dstPath);
                            var prompt = $"파일 '{fileName}'이(가) 대상 폴더에 이미 존재합니다.\n\n" +
                                         $"보내는 쪽 수정일: {srcTime:yyyy-MM-dd HH:mm}\n" +
                                         $"대상 쪽 수정일: {dstTime:yyyy-MM-dd HH:mm}\n\n" +
                                         "덮어쓰시겠습니까?";

                            var ask = MessageBox.Show(prompt, "덮어쓰기 확인", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                            if (ask == DialogResult.No) continue;
                        }

                        File.Copy(srcPath, dstPath, true);
                        success++;
                        continue;
                    }

                    // neither file nor directory
                    errors.Add($"파일 또는 폴더 없음: {fileName}");
                    continue;
                }
                catch (Exception ex)
                {
                    errors.Add($"{fileName}: {ex.Message}");
                }
            }

            // recursive copy helper
            int DirectoryCopy(string sourceDirName, string destDirName, bool overwrite)
            {
                int count = 0;
                var dir = new DirectoryInfo(sourceDirName);
                if (!dir.Exists) throw new DirectoryNotFoundException($"Source directory not found: {sourceDirName}");

                Directory.CreateDirectory(destDirName);

                foreach (var file in dir.GetFiles())
                {
                    var temppath = Path.Combine(destDirName, file.Name);
                    file.CopyTo(temppath, overwrite);
                    count++;
                }

                foreach (var subdir in dir.GetDirectories())
                {
                    var temppath = Path.Combine(destDirName, subdir.Name);
                    count += DirectoryCopy(subdir.FullName, temppath, overwrite);
                }

                return count;
            }

            // Refresh destination list
            try
            {
                LoadDirectoryToListView(destDir, destListView);
            }
            catch { }

            // if there are errors, show them; otherwise don't show a message box
            if (errors.Count > 0)
            {
                var msg = $"복사 완료: {success} 파일\n에러:\n" + string.Join("\n", errors.Take(20));
                if (errors.Count > 20) msg += "\n...";
                MessageBox.Show(msg, "작업 완료", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void btnRightDir_Click(object sender, EventArgs e)
        {
            using (var dlg = new FolderBrowserDialog())
            {
                dlg.Description = "폴더를 선택하세요.";

                // 현재 텍스트박스에 있는 경로를 초기 선택 폴더로 설정
                if (!string.IsNullOrWhiteSpace(txtLeftDir.Text) &&
                                Directory.Exists(txtLeftDir.Text))
                {
                    dlg.SelectedPath = txtLeftDir.Text;
                }

                if (dlg.ShowDialog() == DialogResult.OK)
                {
                    txtRightDir.Text = dlg.SelectedPath;
                    LoadDirectoryToListView(txtRightDir.Text, lvwRightDir);
                    // color matches if left side already loaded
                    CompareAndColorMatches(listView1, lvwRightDir, txtLeftDir.Text, txtRightDir.Text);
                }


            }
        }
    }
}
