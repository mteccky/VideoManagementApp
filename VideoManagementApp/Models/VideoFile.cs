namespace VideoManagementApp.Models
{
    public class VideoFile
    {
        public string FileName { get; set; }
        public long FileSize { get; set; }
        
        public int FileSizeInKB => (int)Math.Round((double)FileSize / 1024);
    }
}
