using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using System.Collections.ObjectModel;
using System.Drawing;
using System.IO;
using System.Windows;
using System.Windows.Media.Imaging;
using System.Diagnostics;

namespace Display_Image_In_DataGridView.ViewModel
{
    public class Simapleata
    {
        public int Number { get; set; }
        public string Name { get; set; }
        public BitmapImage BitmapImg { get; set; }
        public string Path { get; set; }
    }
    public class MainViewModel : ViewModelBase
    {
        private string _taktTime = "";
        public string TackTime
        {
            get { return _taktTime; }
            set { _taktTime = value; RaisePropertyChanged("TackTime"); }
        }

        private ObservableCollection<Simapleata> _collectionFileList = new ObservableCollection<Simapleata>();
        public ObservableCollection<Simapleata> CollectionFileList
        {
            get { return _collectionFileList; }
            set { _collectionFileList = value; }
        }

        public RelayCommand<object> ButtonClickCommand { get; private set; }

        private void InitRelayCommand()
        {
            ButtonClickCommand = new RelayCommand<object>((param) => OnButtonClick(param));
        }
        private void OnButtonClick(object param)
        {
            switch (param.ToString())
            {
                case "DataGrid":
                    RunDataGrid();
                    break;
            }
        }

        private void RunDataGrid()
        {
            string imgDir = @"";

            DirectoryInfo dir = new DirectoryInfo(imgDir);

            int num = 1;

            Stopwatch stopwatch = new Stopwatch();

            stopwatch.Start();
            foreach (FileInfo file in dir.GetFiles())
            {
                CollectionFileList.Add(new Simapleata() { Number = num, Name = file.Name, Path = file.FullName /*BitmapImg = GetBitmapImage(file.FullName)*/ });
                num++;
            }
            stopwatch.Stop();

            TackTime = stopwatch.ElapsedMilliseconds.ToString();

            MessageBox.Show("¿Ï·á");
        }


        public MainViewModel()
        {
            InitRelayCommand();
        }

        private BitmapImage GetBitmapImage(string imagePath)
        {
            if (!File.Exists(imagePath))
                return null;

            Bitmap resultBitmap;
            BitmapImage resultBitmapImage;

            using (FileStream fs = new FileStream(imagePath, FileMode.Open))
            {
                byte[] bytes = new byte[fs.Length];
                fs.Read(bytes, 0, (int)fs.Length);
                using (MemoryStream ms = new MemoryStream(bytes))
                {
                    resultBitmap = Bitmap.FromStream(ms) as Bitmap;
                }
            }

            Bitmap tempBitmap = new Bitmap(resultBitmap);
            resultBitmap.Dispose();

            if (tempBitmap == null)
                return null;

            using (MemoryStream memory = new MemoryStream())
            {
                tempBitmap.Save(memory, System.Drawing.Imaging.ImageFormat.Bmp);
                tempBitmap.Dispose();

                memory.Position = 0;
                resultBitmapImage = new BitmapImage();
                resultBitmapImage.BeginInit();
                resultBitmapImage.StreamSource = memory;
                resultBitmapImage.CacheOption = BitmapCacheOption.OnLoad;
                resultBitmapImage.EndInit();
                resultBitmapImage.Freeze();
            }

            return resultBitmapImage;
        }
    }
}