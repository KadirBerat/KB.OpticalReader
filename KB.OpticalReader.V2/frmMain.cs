using Microsoft.VisualBasic;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO.Pipes;
using System.Linq;
using System.Net.Http.Headers;
using System.Net.NetworkInformation;
using System.Security.Cryptography.Xml;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Formats.Asn1.AsnWriter;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;
using OfficeOpenXml;

namespace KB.OpticalReader.V2
{
    public partial class frmMain : Form
    {
        #region Constructor
        public frmMain()
        {
            InitializeComponent();
        }
        #endregion

        #region Variables
        private static readonly Regex regex = new Regex(@"^\d+$");
        private List<LineModel> lineList = new List<LineModel>();
        private List<LineErrorModel> lineErrorList = new List<LineErrorModel>();
        private List<LineModel> ansKeys = new List<LineModel>();
        private List<ListViewModel> resultList = new List<ListViewModel>();
        private int maxAnswerLength = 0;
        private float questionCount = 0;
        private float examScore = 100f;
        private float coefficient = 0;
        #endregion

        #region Events
        private void tsmiLoadFile_Click(object sender, EventArgs e)
        {
            ClearAll();
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "Optik Okuyucu Sonuç Dosyası (*.txt)|*.txt";
            openFileDialog.Title = "Dosya Yükle";
            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                string path = openFileDialog.FileName;
                if (!String.IsNullOrEmpty(path))
                {
                    string[] lines = File.ReadAllLines(path);
                    foreach (string line in lines)
                    {
                        string _bt = line.Substring(0, 1);
                        string _sn = line.Substring(1, 10);
                        string _ans = line.Substring(11, line.Length - 11);

                        if (_bt == "A" || _bt == "B" || _bt == "C" || _bt == "D" || _bt == "E")
                        {

                            if (regex.IsMatch(_sn))
                            {
                                lineList.Add(new LineModel
                                {
                                    BookletType = _bt,
                                    StudentNumber = _sn,
                                    Answers = _ans
                                });
                            }
                            else
                            {
                                lineErrorList.Add(new LineErrorModel
                                {
                                    BookletType = _bt,
                                    StudentNumber = _sn,
                                    Answers = _ans,
                                    Message = "Öğrenci Numarası Hatalı!"
                                });
                            }
                        }
                        else
                        {
                            lineErrorList.Add(new LineErrorModel
                            {
                                BookletType = _bt,
                                StudentNumber = _sn,
                                Answers = _ans,
                                Message = "Kitapçık Türü Hatalı!"
                            });
                        }


                    }
                    GetAnsKey();

                    if (lineErrorList.Count > 0)
                    {
                        lblError.Visible = true;
                        lblError.Text = "Eksik Veya Hatalı Veri Sayısı: " + lineErrorList.Count.ToString();
                        btnError.Visible = true;
                        btnError.Enabled = true;
                    }
                }
            }

        }
        private void frmMain_Load(object sender, EventArgs e)
        {
            tscmbArrangement.SelectedIndex = 0;
            ExcelPackage.LicenseContext = OfficeOpenXml.LicenseContext.NonCommercial;
        }
        private void frmMain_HelpButtonClicked(object sender, CancelEventArgs e)
        {
            frmHelp frmHelp = new frmHelp();
            frmHelp.ShowDialog();
        }
        private void tstxtCoefficient_TextChanged(object sender, EventArgs e)
        {
            bool isFirst = true;
            bool isFirstComma = true;

            if (!String.IsNullOrEmpty(tstxtCoefficient.Text))
                isFirst = false;

            if (!isFirst)
            {
                if (CountCommas(tstxtCoefficient.Text) > 1)
                    isFirstComma = false;
            }

            string text = tstxtCoefficient.Text;
            string numericText = string.Empty;
            foreach (char c in text)
            {
                if (isFirst)
                {
                    if (char.IsDigit(c))
                    {
                        numericText += c;
                    }
                    isFirst = false;
                }
                else
                {
                    if (isFirstComma)
                    {
                        if (char.IsDigit(c) || c == ',')
                        {
                            numericText += c;
                        }

                        if (c == ',')
                            isFirstComma = false;
                    }
                    else
                    {
                        if (char.IsDigit(c))
                        {
                            numericText += c;
                        }
                    }
                }
            }
            tstxtCoefficient.Text = numericText;
            tstxtCoefficient.SelectionStart = tstxtCoefficient.Text.Length;

            if (!String.IsNullOrEmpty(tstxtCoefficient.Text))
            {
                UpdateSetting(false);
            }
        }
        private void tstxtExamScore_TextChanged(object sender, EventArgs e)
        {
            string text = tstxtExamScore.Text;
            string numericText = string.Empty;
            foreach (char c in text)
            {
                if (char.IsDigit(c))
                {
                    numericText += c;
                }
            }
            tstxtExamScore.Text = numericText;
            tstxtExamScore.SelectionStart = tstxtExamScore.Text.Length;

            if (!String.IsNullOrEmpty(tstxtExamScore.Text))
            {
                if (Convert.ToSingle(tstxtExamScore.Text) > 100f)
                {
                    MessageBox.Show("Sınav Puanı En Fazla 100 Olabilir!" + Environment.NewLine + Environment.NewLine + "Oluşan hata sebebiyle işlem tamamlanamadı lütfen tekrar deneyin.", "Hata!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    examScore = 100f;
                    tstxtExam.Text = "100";
                    tstxtExamScore.Text = "100";
                    UpdateSetting(true);
                }
                else
                {
                    UpdateSetting(true);
                }

            }
        }
        private void tsmiCalculate_Click(object sender, EventArgs e)
        {
            tsmiCalculateSettings.Enabled = false;
            byte indexCounter = 1;

            foreach (var line in lineList)
            {
                line.Answers = line.Answers.Substring(0, maxAnswerLength);

                char[] answerData = line.Answers.ToCharArray();
                char[] keyData = ansKeys.FirstOrDefault(x => x.BookletType == line.BookletType).Answers.ToCharArray();

                int correctCount = 0;
                int wrongCount = 0;
                int blankCount = 0;

                if (answerData.Length < keyData.Length)
                {
                    blankCount = keyData.Length - answerData.Length;

                    for (int i = 0; i < answerData.Length; i++)
                    {
                        if (keyData[i] == answerData[i])
                            correctCount++;
                        else
                        {
                            if (answerData[i] == ' ')
                            {
                                blankCount++;
                            }
                            else
                                wrongCount++;
                        }
                    }
                }
                else
                {
                    for (int i = 0; i < keyData.Length; i++)
                    {
                        if (keyData[i] == answerData[i])
                            correctCount++;
                        else
                        {
                            if (answerData[i] == ' ')
                            {
                                blankCount++;
                            }
                            else
                                wrongCount++;
                        }
                    }
                }

                float score = coefficient * Convert.ToSingle(correctCount);
                float roundedScore = (float)Math.Round(score, 2);

                resultList.Add(new ListViewModel
                {
                    Blank = blankCount,
                    BookletType = line.BookletType,
                    Correct = correctCount,
                    StudentNumber = line.StudentNumber,
                    Wrong = wrongCount,
                    Index = indexCounter,
                    RoundedScore = roundedScore,
                    Score = score
                });

                indexCounter++;
            }

            lvwResults.Items.Clear();
            foreach (var result in resultList)
            {
                ListViewItem listViewItem = new ListViewItem(result.Index.ToString());
                listViewItem.SubItems.Add(result.StudentNumber);
                listViewItem.SubItems.Add(result.BookletType);
                listViewItem.SubItems.Add(result.Correct.ToString());
                listViewItem.SubItems.Add(result.Wrong.ToString());
                listViewItem.SubItems.Add(result.Blank.ToString());
                listViewItem.SubItems.Add(result.RoundedScore.ToString());
                listViewItem.SubItems.Add(result.Score.ToString());
                lvwResults.Items.Add(listViewItem);
            }

            lblStudentCount.Text = "Sınava Giren Öğrenci Sayısı: " + resultList.Count().ToString();
            lblAverage.Text = "Sınıf Ortalaması: " + resultList.Average(x => x.Score).ToString();
            lblMax.Text = "En Yüksek Puan: " + resultList.OrderBy(x => x.Score).LastOrDefault().Score.ToString();
            lblMin.Text = "En Düşük Puan: " + resultList.OrderBy(x => x.Score).FirstOrDefault().Score.ToString();

            tsmiArrangement.Enabled = true;
            tsmiExportToExcel.Enabled = true;

        }

        private void tscmbArrangement_SelectedIndexChanged(object sender, EventArgs e)
        {
            List<ListViewModel> sortedList = new List<ListViewModel>();

            if (tscmbArrangement.SelectedIndex == 0)
            {
                sortedList = resultList;
            }
            else if (tscmbArrangement.SelectedIndex == 1)
            {
                sortedList = resultList.OrderBy(x => x.BookletType).ToList();
            }
            else if (tscmbArrangement.SelectedIndex == 2)
            {
                sortedList = resultList.OrderBy(x => x.Correct).ToList();
            }
            else if (tscmbArrangement.SelectedIndex == 3)
            {
                sortedList = resultList.OrderByDescending(x => x.Correct).ToList();
            }
            else if (tscmbArrangement.SelectedIndex == 4)
            {
                sortedList = resultList.OrderBy(x => x.Wrong).ToList();
            }
            else if (tscmbArrangement.SelectedIndex == 5)
            {
                sortedList = resultList.OrderByDescending(x => x.Wrong).ToList();
            }
            else if (tscmbArrangement.SelectedIndex == 6)
            {
                sortedList = resultList.OrderBy(x => x.Blank).ToList();
            }
            else if (tscmbArrangement.SelectedIndex == 7)
            {
                sortedList = resultList.OrderByDescending(x => x.Blank).ToList();
            }
            else if (tscmbArrangement.SelectedIndex == 8)
            {
                sortedList = resultList.OrderBy(x => x.Score).ToList();
            }
            else if (tscmbArrangement.SelectedIndex == 9)
            {
                sortedList = resultList.OrderByDescending(x => x.Score).ToList();
            }

            lvwResults.Items.Clear();
            foreach (var data in sortedList)
            {
                ListViewItem listViewItem = new ListViewItem(data.Index.ToString());
                listViewItem.SubItems.Add(data.StudentNumber);
                listViewItem.SubItems.Add(data.BookletType);
                listViewItem.SubItems.Add(data.Correct.ToString());
                listViewItem.SubItems.Add(data.Wrong.ToString());
                listViewItem.SubItems.Add(data.Blank.ToString());
                listViewItem.SubItems.Add(data.RoundedScore.ToString());
                listViewItem.SubItems.Add(data.Score.ToString());
                lvwResults.Items.Add(listViewItem);
            }
        }
        private void btnError_Click(object sender, EventArgs e)
        {
            string textData = String.Empty;
            foreach (var item in lineErrorList)
            {
                item.Answers = item.Answers.Substring(0, maxAnswerLength);
                textData += $"Öğrenci Numarası: {item.StudentNumber}, Kitapçık Türü: {item.BookletType}, Cevaplar: {item.Answers} \nMesaj: {item.Message}\n\n";
            }
            MessageBox.Show(textData, "Eksik Veya Hatalı Veriler", MessageBoxButtons.OK, MessageBoxIcon.Warning);
        }
        private void tsmiExportToExcel_Click(object sender, EventArgs e)
        {
            SaveFileDialog saveFileDialog = new SaveFileDialog();
            saveFileDialog.Filter = "Excel Dosyası|*.xlsx";

            if (saveFileDialog.ShowDialog() == DialogResult.OK)
            {
                this.Enabled = false;
                string filePath = saveFileDialog.FileName;

                FileInfo newFile = new FileInfo(filePath);

                using (ExcelPackage package = new ExcelPackage(newFile))
                {
                    ExcelWorksheet worksheet = package.Workbook.Worksheets.Add("Sonuçlar");

                    for (int i = 0; i < lvwResults.Columns.Count; i++)
                    {
                        worksheet.Cells[1, i + 1].Value = lvwResults.Columns[i].Text;
                        worksheet.Column(i + 1).AutoFit();
                    }

                    for (int i = 0; i < lvwResults.Items.Count; i++)
                    {
                        for (int j = 0; j < lvwResults.Items[i].SubItems.Count; j++)
                        {
                            worksheet.Cells[i + 2, j + 1].Value = lvwResults.Items[i].SubItems[j].Text;
                            worksheet.Column(j + 1).AutoFit();
                        }
                    }
                    worksheet.Cells[1, 10].Value = "Sınava Giren Öğrenci Sayısı: ";
                    worksheet.Cells[1, 11].Value = resultList.Count().ToString();
                    worksheet.Cells[2, 10].Value = "Sınıf Ortalaması: ";
                    worksheet.Cells[2, 11].Value = resultList.Average(x => x.Score).ToString();
                    worksheet.Cells[3, 10].Value = "En Yüksek Puan: ";
                    worksheet.Cells[3, 11].Value = resultList.OrderBy(x => x.Score).LastOrDefault().Score.ToString();
                    worksheet.Cells[4, 10].Value = "En Düşük Puan: ";
                    worksheet.Cells[4, 11].Value = resultList.OrderBy(x => x.Score).FirstOrDefault().Score.ToString();
                    worksheet.Column(10).AutoFit();
                    worksheet.Column(11).AutoFit();

                    if (lineErrorList.Count > 0)
                    {
                        ExcelWorksheet errWorksheet = package.Workbook.Worksheets.Add("Eksik Veya Hatalı Veriler");
                        errWorksheet.Cells[1, 1].Value = "Öğrenci Numarası";
                        errWorksheet.Cells[1, 2].Value = "Kitapçık Türü";
                        errWorksheet.Cells[1, 3].Value = "Cevaplar";
                        errWorksheet.Cells[1, 4].Value = "Mesaj";
                        for (int i = 0; i < lineErrorList.Count; i++)
                        {
                            errWorksheet.Cells[i + 2, 1].Value = lineErrorList[i].StudentNumber;
                            errWorksheet.Cells[i + 2, 2].Value = lineErrorList[i].BookletType;
                            errWorksheet.Cells[i + 2, 3].Value = lineErrorList[i].Answers;
                            errWorksheet.Cells[i + 2, 4].Value = lineErrorList[i].Message;
                        }
                        errWorksheet.Column(1).AutoFit();
                        errWorksheet.Column(2).AutoFit();
                        errWorksheet.Column(3).AutoFit();
                        errWorksheet.Column(4).AutoFit();
                    }

                    ExcelWorksheet snpWorksheet = package.Workbook.Worksheets.Add("Öğrenci Numarası + Ham Puan");
                    snpWorksheet.Cells[1, 1].Value = "Öğrenci Numarası";
                    snpWorksheet.Cells[1, 2].Value = "Ham Puan";
                    for (int i = 0; i < resultList.Count; i++)
                    {
                        snpWorksheet.Cells[i + 2, 1].Value = resultList[i].StudentNumber;
                        snpWorksheet.Cells[i + 2, 2].Value = resultList[i].Score.ToString();
                    }
                    snpWorksheet.Column(1).AutoFit();
                    snpWorksheet.Column(2).AutoFit();


                    ExcelWorksheet snrpWorksheet = package.Workbook.Worksheets.Add("Öğrenci Numarası + Puan");
                    snrpWorksheet.Cells[1, 1].Value = "Öğrenci Numarası";
                    snrpWorksheet.Cells[1, 2].Value = "Puan";
                    for (int i = 0; i < resultList.Count; i++)
                    {
                        snrpWorksheet.Cells[i + 2, 1].Value = resultList[i].StudentNumber;
                        snrpWorksheet.Cells[i + 2, 2].Value = resultList[i].RoundedScore.ToString();
                    }
                    snrpWorksheet.Column(1).AutoFit();
                    snrpWorksheet.Column(2).AutoFit();

                    package.Save();
                }

                MessageBox.Show("Excel Dosyası Oluşturuldu.", "Başarılı", MessageBoxButtons.OK, MessageBoxIcon.Information);
                this.Enabled = true;
            }
        }
        #endregion

        #region Methods
        private void ClearAll()
        {
            lineList.Clear();
            lineErrorList.Clear();
            ansKeys.Clear();
            lvwResults.Items.Clear();
            resultList.Clear();
            examScore = 100f;
            coefficient = 0;
            questionCount = 0;

            tstxtCount.Text = questionCount.ToString();
            tstxtQuestion.Text = coefficient.ToString();
            tstxtExam.Text = examScore.ToString();

            tstxtExamScore.Text = examScore.ToString();
            tstxtCoefficient.Text = coefficient.ToString();

            tsmiCalculate.Enabled = false;
            tsmiCalculateSettings.Enabled = false;
            tsmiArrangement.Enabled = false;
            tsmiExportToExcel.Enabled = false;

        }
        private void GetAnsKey()
        {
            List<LineModel> ansKeyList = lineList.Where(x => x.StudentNumber == "0000000000").ToList();
            if (ansKeyList.Count > 0)
            {
                foreach (LineModel line in ansKeyList)
                {
                    LineModel ans = line;
                    ans.Answers = ans.Answers.Replace(" ", "");
                    ansKeys.Add(ans);
                    lineList.Remove(line);
                }
                tsmiCalculateSettings.Enabled = true;
                tsmiCalculate.Enabled = true;
                CalculateSettings();
            }
            else
            {
                ClearAll();
                MessageBox.Show("Cevap Anahtarı Eksik!");
            }
        }
        private void CalculateSettings()
        {
            maxAnswerLength = ansKeys.Max(key => key.Answers.Length);
            questionCount = maxAnswerLength;
            coefficient = 100 / questionCount;

            UpdateSettingVals();
        }
        private void UpdateSetting(bool isExam)
        {
            if (isExam)
            {
                examScore = Convert.ToSingle(tstxtExamScore.Text);
                coefficient = examScore / questionCount;
                tstxtQuestion.Text = coefficient.ToString();
                tstxtExam.Text = examScore.ToString();
                tstxtCoefficient.Text = coefficient.ToString();
            }
            else
            {
                coefficient = Convert.ToSingle(tstxtCoefficient.Text);
                examScore = coefficient * Convert.ToSingle(questionCount);
                if (FloatControl(examScore))
                {
                    tsmiCalculate.Enabled = false;
                    tsmiCalculateSettings.Enabled = false;

                    MessageBox.Show("Sınav Puanı Tam Sayı Olmak Zorunda!" + Environment.NewLine + Environment.NewLine + "Oluşan hata sebebiyle işlem tamamlanamadı lütfen tekrar deneyin.", "Hata!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    examScore = 100f;
                    coefficient = examScore / questionCount;

                    tstxtExamScore.Text = examScore.ToString();
                    tstxtCoefficient.Text = coefficient.ToString();
                }
                else
                {
                    tstxtExamScore.Text = examScore.ToString();
                }
            }
        }
        private void UpdateSettingVals()
        {
            tstxtCount.Text = questionCount.ToString();
            tstxtQuestion.Text = coefficient.ToString();
            tstxtExam.Text = examScore.ToString();

            tstxtExamScore.Text = examScore.ToString();
            tstxtCoefficient.Text = coefficient.ToString();
        }
        private int CountCommas(string text)
        {
            int count = 0;

            foreach (char c in text)
            {
                if (c == ',')
                {
                    count++;
                }
            }

            return count;
        }
        private bool FloatControl(float value)
        {
            if (value.ToString().Contains(',') || value.ToString().Contains('.'))
                return true;
            else
                return false;
        }
        #endregion

    }

    #region Model
    internal class LineModel
    {
        public string BookletType { get; set; }
        public string StudentNumber { get; set; }
        public string Answers { get; set; }
    }
    internal class LineErrorModel
    {
        public string BookletType { get; set; }
        public string StudentNumber { get; set; }
        public string Answers { get; set; }
        public string Message { get; set; }
    }
    internal class ListViewModel
    {
        public int Index { get; set; }
        public string StudentNumber { get; set; }
        public string BookletType { get; set; }
        public int Correct { get; set; }
        public int Wrong { get; set; }
        public int Blank { get; set; }
        public float Score { get; set; }
        public float RoundedScore { get; set; }
    }
    #endregion
}
