using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Diagnostics;
using System.IO;
using System.Text.RegularExpressions;

namespace DataStructure_Project_UAS
{
    public partial class Form : System.Windows.Forms.Form
    {
        int[] data;
        int[] sorted;
        double timeElapsed = 0;
        double count = 0;

        public string Title { get; private set; }
        public string Filter { get; private set; }
        public string FileName { get; private set; }

        public Form()
        {
            InitializeComponent();
        }

        private void Form_Load(object sender, EventArgs e)
        {
            labelAccuracy.Text = "?";
            labelPecahan.Text = "?";
            labelTimeElapsed.Text = "?";
        }

        private void buttonBrowse_Click(object sender, EventArgs e)
        {
            //Membuat instance OpenFileDialog
            OpenFileDialog openFileDialog = new OpenFileDialog();

            //Konfigurasi opsi dialog
            openFileDialog.Title = "Select a Text File";
            openFileDialog.Filter = "Text Files (*.txt)|*.txt";

            //Menampilkan dialog dan mengecek apakah user memilih file
            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                //Mengambil path file dan menampilkannya di TextBox
                textBoxInputPathLink.Text = openFileDialog.FileName;

                //Membaca data dari file text
                try
                {
                    string fileContent = File.ReadAllText(openFileDialog.FileName);

                    //Mengubah data menjadi array angka
                    data = ParseNumbersFromText(fileContent);
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Error reading file: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            listBoxOutput.DataSource = data;
        }

        private int[] ParseNumbersFromText(string content)
        {
            //Memisahkan text menggunakan spasi, koma, baris baru
            string[] parts = content.Split(new[] { ' ', ',', '\n', '\r',';','"' }, StringSplitOptions.RemoveEmptyEntries);

            //Mengkonversi string ke array angka
            return parts.Select(int.Parse).ToArray();
        }

        private void buttonSort_Click(object sender, EventArgs e)
        {
            bool descend = false;
            if (data == null || data.Length == 0)
            {
                MessageBox.Show("No data loaded. Please selec a valid file", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            if (comboBoxSortMethod.SelectedIndex == -1 || comboBoxOrder.SelectedIndex == -1)
            {
                MessageBox.Show("Please select both sorting method and order", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            if (comboBoxSortMethod.SelectedIndex == 0 && comboBoxOrder.SelectedIndex == 0)
            {
                var watch = System.Diagnostics.Stopwatch.StartNew();
                sorted = Sort.InsertionSort(data);
                watch.Stop();
                timeElapsed = watch.ElapsedMilliseconds;
            }
            else if (comboBoxSortMethod.SelectedIndex == 0 && comboBoxOrder.SelectedIndex == 1)
            {
                var watch = System.Diagnostics.Stopwatch.StartNew();
                sorted = Sort.InsertionSort(data);
                watch.Stop();
                timeElapsed = watch.ElapsedMilliseconds;
                sorted = Descender(sorted);
                descend = true;
            }
            else if (comboBoxSortMethod.SelectedIndex == 1 && comboBoxOrder.SelectedIndex == 0)
            {
                var watch = System.Diagnostics.Stopwatch.StartNew();
                sorted = Sort.BubbleSort(data);
                watch.Stop();
                timeElapsed = watch.ElapsedMilliseconds;
            }
            else if (comboBoxSortMethod.SelectedIndex == 1 && comboBoxOrder.SelectedIndex == 1)
            {
                var watch = System.Diagnostics.Stopwatch.StartNew();
                sorted = Sort.BubbleSort(data);
                watch.Stop();
                timeElapsed = watch.ElapsedMilliseconds;
                sorted = Descender(sorted);
                descend = true;
            }
            else if (comboBoxSortMethod.SelectedIndex == 2 && comboBoxOrder.SelectedIndex == 0)
            {
                var watch = System.Diagnostics.Stopwatch.StartNew();
                sorted = Sort.HeapSort(data);
                watch.Stop();
                timeElapsed = watch.ElapsedMilliseconds;
            }
            else if (comboBoxSortMethod.SelectedIndex == 2 && comboBoxOrder.SelectedIndex == 1)
            {
                var watch = System.Diagnostics.Stopwatch.StartNew();
                sorted = Sort.HeapSort(data);
                watch.Stop();
                timeElapsed = watch.ElapsedMilliseconds;
                sorted = Descender(sorted);
                descend = true;
            }
            else if (comboBoxSortMethod.SelectedIndex == 3 && comboBoxOrder.SelectedIndex == 0)
            {
                var watch = System.Diagnostics.Stopwatch.StartNew();
                sorted = Sort.RadixSort(data);
                watch.Stop();
                timeElapsed = watch.ElapsedMilliseconds;
            }
            else if (comboBoxSortMethod.SelectedIndex == 3 && comboBoxOrder.SelectedIndex == 1)
            {
                var watch = System.Diagnostics.Stopwatch.StartNew();
                sorted = Sort.RadixSort(data);
                watch.Stop();
                timeElapsed = watch.ElapsedMilliseconds;
                sorted = Descender(sorted);
                descend = true;
            }
            else if (comboBoxSortMethod.SelectedIndex == 4 && comboBoxOrder.SelectedIndex == 0)
            {
                var watch = System.Diagnostics.Stopwatch.StartNew();
                sorted = Sort.MergeSort(data);
                watch.Stop();
                timeElapsed = watch.ElapsedMilliseconds;
            }
            else if (comboBoxSortMethod.SelectedIndex == 4 && comboBoxOrder.SelectedIndex == 1)
            {
                var watch = System.Diagnostics.Stopwatch.StartNew();
                sorted = Sort.MergeSort(data);
                watch.Stop();
                timeElapsed = watch.ElapsedMilliseconds;
                sorted = Descender(sorted);
                descend = true;
            }
            else
            {
                MessageBox.Show("Invalid selection. Please check your options.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            //Listbox Output
            labelOutput.Text = "DISPLAY OUTPUT : ";
            listBoxOutput.DataSource = null;
            listBoxOutput.DataSource = sorted;

            //Accuracy
            count = SortChecker(sorted,descend);
            double percentage = (count/sorted.Length) * 100;
            labelAccuracy.Text = percentage.ToString();
            labelPecahan.Text = count.ToString();
            labelJumlahData.Text = "/"+ sorted.Length + ")";

            //Time Elapsed
            labelTimeElapsed.Text = timeElapsed.ToString();
        }

        private void buttonDownload_Click(object sender, EventArgs e)
        {
            try
            {
                //Memastikan ada data yang mau di save
                if (listBoxOutput.Items.Count == 0)
                {
                    MessageBox.Show("No data to save. Please sort the data first,", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                //Create a SaveFileDialog
                SaveFileDialog saveFileDialog = new SaveFileDialog();
                {
                    Title = "Save Sorted Data";
                    Filter = "Text Files (*.txt)|*.txt";
                    FileName = "SortedData.txt"; //defaulted file name
                }

                //Show the SaveFileDialog
                if (saveFileDialog.ShowDialog() == DialogResult.OK)
                {
                    //Get the selected file path
                    string filePath = saveFileDialog.FileName + ".txt";

                    //Write the ListBox items to the file
                    using (StreamWriter writer = new StreamWriter(filePath))
                    {
                        foreach (var item in listBoxOutput.Items)
                        {
                            writer.WriteLine(item);
                        }
                    }
                    MessageBox.Show("Data successfully saved!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"An error occurred while saving the file: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void buttonClear_Click(object sender, EventArgs e)
        {
            textBoxInputPathLink.Text = "input file";
            comboBoxSortMethod.SelectedIndex = -1;
            comboBoxOrder.SelectedIndex = -1;
            listBoxOutput.DataSource = null;
            textBoxFileName.Text = "file name";
            labelAccuracy.Text = "?";
            labelPecahan.Text = "?";
            labelTimeElapsed.Text = "?";
        }

        private void buttonExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        #region DIAGNOSTICS
        private double SortChecker(int[] dataset,bool descend)
        {
            if(descend)
            {
                double count = 1;
                for (int i = 0; i < (dataset.Length - 1); i++)
                {
                    if (dataset[i] >= dataset[i + 1])
                    {
                        count++;
                    }
                }
                return count;
            }
            else
            {
                double count = 1;
                for (int i = 0; i < (dataset.Length - 1); i++)
                {
                    if (dataset[i] <= dataset[i + 1])
                    {
                        count++;
                    }
                }
                return count;
            }
        }
        #endregion


        #region DESCENDING
        private int[] Descender(int[] dataset)
        {
            int[] result = new int[dataset.Length];

            int index = 0;
            for (int i = dataset.Length - 1; i > 0; i--)
            {
                result[index] = dataset[i];
                index++;
            }
            return result;
        }
        #endregion
    }
}
