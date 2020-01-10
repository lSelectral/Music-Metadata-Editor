namespace MediaEditor
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Windows;
    using System.Windows.Forms;
    using System.Resources;
    using MessageBox = System.Windows.Forms.MessageBox;
    using ContextMenu = System.Windows.Controls.ContextMenu;
    using System.Linq;
    using System.Windows.Input;
    using DragDropEffects = System.Windows.DragDropEffects;
    using DataObject = System.Windows.DataObject;
    using MouseEventArgs = System.Windows.Input.MouseEventArgs;
    using DataFormats = System.Windows.DataFormats;

    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        // Selected folder path
        public string selectedPath = null;
        // Selected file paths in the selected folder
        public string[] selectedFiles = null;

        /// <summary>
        /// Initialize the main window of the form
        /// </summary>
        public MainWindow()
        {
            // Form initialization
            InitializeComponent();
            // Delete key event
            FileList.KeyDown += DeleteKeyPressed_KeyDown;
        }

        private void selectFolderButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                // Set the properties of folder browser dialog
                FolderBrowserDialog folderBrowserDialog = new FolderBrowserDialog();
                folderBrowserDialog.ShowNewFolderButton = false;
                folderBrowserDialog.Description = "Please select a folder that contain any media file";
                folderBrowserDialog.ShowDialog();
                
                // Set the selected path dialog
                selectedPath = folderBrowserDialog.SelectedPath;

                if (selectedPath != null && selectedPath != "")
                {
                    // Collect all selected files
                    selectedFiles = Directory.GetFiles(selectedPath, "*.mp3");
                    if (selectedPath != null && selectedPath != "")
                    {
                        updateTheList(selectedFiles);
                    }
                    else
                    {
                        MessageBox.Show("This directory contain no media file.", "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
            catch (OperationCanceledException)
            {
                throw;
            }
            catch  (DirectoryNotFoundException)
            {
                MessageBox.Show("ERROR","Selected Folder Directory Contain no media file.");
                throw;
            }
        }

        
        /// <summary>
        /// Update the selected media info as user wish
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void updateButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                foreach (var item in FileList.SelectedItems)
                {
                    var mediaItem = item as MediaInfo;
                    var path = mediaItem.Path;
                    var tFile = TagLib.File.Create(path);

                    // Old infos of medias
                    var artistName = tFile.Tag.Performers;
                    var genre = tFile.Tag.Genres;
                    var title = tFile.Tag.Title;
                    var album = tFile.Tag.Album;

                    var newArtistName = artistTextBox.Text;
                    var newGenre = genreTextBox.Text;
                    var newTitle = titleTextBox.Text;
                    var newAlbum = albumTextBox.Text;

                    if (artistTextBox.Text == "{ALBUM}")
                    {
                        newArtistName = album;
                    }
                    if (albumTextBox.Text == "{ARTIST}")
                    {
                        newAlbum = artistName.First();
                    }
                    if (albumTextBox.Text == "{TITLE}")
                    {
                        newAlbum = title;
                    }

                    if (newArtistName != null && newArtistName != "")
                    {
                        var tempList = new List<string>() { newArtistName };
                        tFile.Tag.Performers = tempList.ToArray();
                    }


                    if (newGenre != null && newGenre != "")
                    {
                        var tempList2 = new List<string>() { newGenre };
                        tFile.Tag.Genres = tempList2.ToArray();
                    }

                    if (newAlbum != null && newAlbum != "")
                        tFile.Tag.Album = newAlbum;

                    if (newTitle != null && newTitle != "")
                        tFile.Tag.Title = newTitle;

                    tFile.Save();
                }

                FileList.Items.Clear();
                updateTheList(selectedFiles);
            }
            catch (NullReferenceException)
            {
                MessageBox.Show("There is no selected media file in the list", "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            catch (ArgumentNullException)
            {
                MessageBox.Show("There is no selected media file in the list", "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// Create a backup file in the user selected path
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void makeBackUp_Click(object sender, RoutedEventArgs e)
        {
            createCopy();
        }

        /// <summary>
        /// Clear the content of the list
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void clearTheList_Click(object sender, RoutedEventArgs e)
        {
            FileList.Items.Clear();
        }

        /// <summary>
        /// Update the list objects method
        /// </summary>
        /// <param name="mediaFiles">An array that contain mp3 file paths</param>
        private void updateTheList(string[] mediaFiles)
        {
            try
            {
                foreach (var file in selectedFiles)
                {
                    FileInfo f = new FileInfo(file);
                    if (f.Extension != ".mp3")
                    {
                        continue;
                    }

                    // Create the file as TagLib File
                    var tFile = TagLib.File.Create(file);

                    var artistName = tFile.Tag.FirstPerformer;
                    var genre = tFile.Tag.FirstGenre;
                    var title = tFile.Tag.Title;
                    var path = file;
                    var album = tFile.Tag.Album;

                    FileList.Items.Add(new MediaInfo { Title = title, Artist = artistName, Genre = genre, Path = path, Album = album });
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        private void createCopy()
        {


            FolderBrowserDialog folderBrowserDialog = new FolderBrowserDialog();
            folderBrowserDialog.Description = "Please select a folder to copy the files";
            folderBrowserDialog.ShowDialog();
            try
            {
                var selectedFolder = folderBrowserDialog.SelectedPath;

                foreach (var item in FileList.SelectedItems)
                {
                    var mediaInfo = item as MediaInfo;
                    var name = mediaInfo.Title;

                    var path = mediaInfo.Path;

                    File.Copy(path, selectedFolder + "/" + name + ".mp3", true);
                }

                MessageBox.Show("Backup of the media files saved to: " + selectedFolder, "INFO", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (NullReferenceException)
            {
                MessageBox.Show("There is no selected media file in the list", "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// Delete key down event for deleting the selected files in the media list
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void DeleteKeyPressed_KeyDown(object sender, System.Windows.Input.KeyEventArgs e)
        {
            try
            {
                // Create a temporary list for adding selected items.
                // Directly delete selected items cause a crash. (Static)
                List<object> tempList = new List<object>();

                foreach (var item in FileList.SelectedItems)
                {
                    tempList.Add(item);
                }
                foreach (var item in tempList)
                {
                    // Control if "DEL" key down
                    if (e.Key == System.Windows.Input.Key.Delete)
                    {
                        FileList.Items.Remove(item);
                    }
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

         /// <summary>
         /// Copy files context menu
         /// </summary>
         /// <param name="sender"></param>
         /// <param name="e"></param>
        private void CopyContextMenu_Click(object sender, RoutedEventArgs e)
        {
            
        }

        /// <summary>
        /// Paste files context menu
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void PasteContextMenu_Click(object sender, RoutedEventArgs e)
        {
            createCopy();
        }

        /// <summary>
        /// Open root folder context menu
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void OpenContextMenu_Click(object sender, RoutedEventArgs e)
        {
            foreach (var item in FileList.SelectedItems)
            {
                var mediaInfo = item as MediaInfo;
                var path = mediaInfo.Path;

                OpenFileDialog fileDialog = new OpenFileDialog();
                if (File.Exists(path))
                {
                    fileDialog.InitialDirectory = new FileInfo(path).Directory.FullName;
                    fileDialog.FileName = path;
                    fileDialog.ShowReadOnly = false;
                    fileDialog.Multiselect = false;
                    fileDialog.ShowDialog();
                }
            }
        }

        private void DeletePermanently_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                DialogResult q = MessageBox.Show("Are you sure? This file will be removed permanently", "WARNING", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);

                if (q == System.Windows.Forms.DialogResult.Yes)
                {
                    foreach (var item in FileList.SelectedItems)
                    {
                        var mediaInfo = item as MediaInfo;
                        var path = mediaInfo.Path;
                        File.Delete(path);
                    }
                }
            }
            catch (Exception)
            {

                throw;
            }
        }

        private void Window_Drop(object sender, System.Windows.DragEventArgs e)
        {
            string[] droppedFiles = null;
            if (e.Data.GetDataPresent(DataFormats.FileDrop))
            {
                droppedFiles = e.Data.GetData(DataFormats.FileDrop, true) as string[];
            }

            if ((null == droppedFiles) || (!droppedFiles.Any())) { return; }
            selectedFiles = droppedFiles;

            updateTheList(droppedFiles);

        }

        private void Button_Click()
        {

        }
    }
}
