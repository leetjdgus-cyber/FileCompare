using System.IO;
using System.Globalization;
using System.Drawing;

namespace FileCompare
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
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
                var files = dirInfo.GetFiles();

                // ensure columns alignment
                if (lv.Columns.Count >= 3)
                {
                    lv.Columns[0].TextAlign = HorizontalAlignment.Left;
                    lv.Columns[1].TextAlign = HorizontalAlignment.Right;
                    lv.Columns[2].TextAlign = HorizontalAlignment.Right;
                }

                int idx = 0;
                foreach (var f in files)
                {
                    var item = new ListViewItem(f.Name);
                    item.SubItems.Add(FormatFileSize(f.Length));
                    item.SubItems.Add(f.LastWriteTime.ToString("yyyy-MM-dd HH:mm", CultureInfo.CurrentCulture));

                    // unified coloring: use a palette so rows show a variety of colors consistently
                    Color[] palette = new Color[]
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

                    var back = palette[idx % palette.Length];
                    item.BackColor = back;

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
                    idx++;
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
                    if (!File.Exists(srcPath))
                    {
                        errors.Add($"파일 없음: {fileName}");
                        continue;
                    }

                    if (File.Exists(dstPath))
                    {
                        var srcTime = File.GetLastWriteTime(srcPath);
                        var dstTime = File.GetLastWriteTime(dstPath);
                        var prompt = $"파일 '{fileName}'이(가) 대상 폴더에 이미 존재합니다.\n\n" +
                                     $"보내는 쪽 수정일: {srcTime:yyyy-MM-dd HH:mm}\n" +
                                     $"대상 쪽 수정일: {dstTime:yyyy-MM-dd HH:mm}\n\n" +
                                     "덮어쓰시겠습니까?";

                        var ask = MessageBox.Show(prompt,
                                                   "덮어쓰기 확인",
                                                   MessageBoxButtons.YesNo,
                                                   MessageBoxIcon.Question);
                        if (ask == DialogResult.No)
                        {
                            // skip this file
                            continue;
                        }
                    }

                    File.Copy(srcPath, dstPath, true);
                    success++;
                }
                catch (Exception ex)
                {
                    errors.Add($"{fileName}: {ex.Message}");
                }
            }

            // Refresh destination list
            try
            {
                LoadDirectoryToListView(destDir, destListView);
            }
            catch { }

            var msg = $"복사 완료: {success} 파일";
            if (errors.Count > 0)
            {
                msg += "\n에러:\n" + string.Join("\n", errors.Take(10));
                if (errors.Count > 10) msg += "\n...";
            }

            MessageBox.Show(msg, "작업 완료", MessageBoxButtons.OK, MessageBoxIcon.Information);
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
                }


            }
        }
    }
}
