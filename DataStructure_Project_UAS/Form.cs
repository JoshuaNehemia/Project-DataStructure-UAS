using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;

namespace DataStructure_Project_UAS
{
    public partial class Form : System.Windows.Forms.Form
    {
        int[] data;
        int[] sorted;

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
        }

        private int[] ParseNumbersFromText(string content)
        {
            //Memisahkan text menggunakan spasi, koma, baris baru
            string[] parts = content.Split(new[] { ' ', ',', '\n', '\r' }, StringSplitOptions.RemoveEmptyEntries);

            //Mengkonversi string ke array angka
            return parts.Select(int.Parse).ToArray();
        }

        private void buttonSort_Click(object sender, EventArgs e)
        {
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
                sorted = Sort.InsertionSort(data);
            }
            else if (comboBoxSortMethod.SelectedIndex == 0 && comboBoxSortMethod.SelectedIndex == 1)
            {

            }
            else if (comboBoxSortMethod.SelectedIndex == 1 && comboBoxOrder.SelectedIndex == 0)
            {
                sorted = Sort.BubbleSort(data);
            }
            else if (comboBoxSortMethod.SelectedIndex == 1 && comboBoxOrder.SelectedIndex == 1)
            {

            }
            else if (comboBoxSortMethod.SelectedIndex == 2 && comboBoxOrder.SelectedIndex == 0)
            {
                sorted = Sort.HeapSort(data);
            }
            else if (comboBoxSortMethod.SelectedIndex == 2 && comboBoxOrder.SelectedIndex == 1)
            {

            }
            else if (comboBoxSortMethod.SelectedIndex == 3 && comboBoxOrder.SelectedIndex == 0)
            {
                sorted = Sort.RadixSort(data);
            }
            else if (comboBoxSortMethod.SelectedIndex == 3 && comboBoxOrder.SelectedIndex == 1)
            {

            }
            else if (comboBoxSortMethod.SelectedIndex == 4 && comboBoxOrder.SelectedIndex == 0)
            {
                sorted = Sort.MergeSort(data);
            }
            else if (comboBoxSortMethod.SelectedIndex == 4 && comboBoxOrder.SelectedIndex == 1)
            {

            }
            else
            {
                MessageBox.Show("Invalid selection. Please check your options.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            listBoxOutput.Text = string.Join(", ", sorted);

            labelAccuracy.Text = 0.ToString();
            labelPecahan.Text = 0.ToString();
            labelTimeElapsed.Text = 0.ToString();
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
                    string filePath = saveFileDialog.FileName;

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
            listBoxOutput.Items.Clear();
            textBoxFileName.Text = "file name";
            labelAccuracy.Text = "?";
            labelPecahan.Text = "?";
            labelTimeElapsed.Text = "?";
        }

        private void buttonExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
