using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;

using Livet;
using Livet.Commands;
using Livet.Messaging;
using Livet.Messaging.IO;
using Livet.EventListeners;
using Livet.Messaging.Windows;

using LivetApp1.Models;
using Microsoft.Win32;
using System.Data;
using LivetApp1.Services;
using System.Windows;
using ClosedXML.Excel;

namespace LivetApp1.ViewModels
{
    public class ExceloutputViewModel : ViewModel
    {
        private SaveFileDialog dialog = new SaveFileDialog()
                                                            {
            Filter = "Excelファイル(.xlsx)|*.xlsx"
        };

        private IRestService service = new RestService();

        #region TERM
        private Term _Term;

        public Term Term
        {
            get
            { return _Term; }
            set
            {
                if (_Term == value)
                    return;
                _Term = value;
                RaisePropertyChanged();
            }
        }
        #endregion

        #region SavePath
        private string _SavePath;

        public string SavePath
        {
            get
            { return _SavePath; }
            set
            {
                if (_SavePath == value)
                    return;
                _SavePath = value;
                RaisePropertyChanged();
            }
        }
        #endregion

        #region ThanksCard
        private List<ThanksCard> _ThanksCards;

        public List<ThanksCard> ThanksCards
        {
            get
            { return _ThanksCards; }
            set
            {
                if (_ThanksCards == value)
                    return;
                _ThanksCards = value;
                RaisePropertyChanged();
            }
        }
        #endregion

        #region Close

        private ViewModelCommand _CloseCommand;

        public ViewModelCommand CloseCommand
        {
            get
            {
                if (_CloseCommand == null)
                {
                    _CloseCommand = new ViewModelCommand(Close);
                }
                return _CloseCommand;
            }
        }

        public void Close()
        {
            Messenger.Raise(new WindowActionMessage(WindowAction.Close, "Close"));
        }



        #endregion

        #region SelectSavePathCommand

        private ViewModelCommand _SelectSavePathCommand;

        public ViewModelCommand SelectSavePathCommand
        {
            get
            {
                if (_SelectSavePathCommand == null)
                {
                    _SelectSavePathCommand = new ViewModelCommand(SelectSavePath);
                }
                return _SelectSavePathCommand;
            }
        }

        public void SelectSavePath()
        {
            dialog.FileName = "";
            if ((bool)dialog.ShowDialog())
            {
                this.SavePath = dialog.FileName ;
            }
        }

        #endregion

        #region ExecuteCommand

        private ViewModelCommand _ExecuteCommand;

        public ViewModelCommand ExecuteCommand
        {
            get
            {
                if (_ExecuteCommand == null)
                {
                    _ExecuteCommand = new ViewModelCommand(Execute);
                }
                return _ExecuteCommand;
            }
        }

        public async void Execute()
        {
            string error = InputCheck();
            if (string.IsNullOrEmpty(error))
            {
                ThanksCards = await service.GetCardsForReport(this.Term);
                if (ThanksCards.Count != 0)
                {
                    IXLWorkbook book = new XLWorkbook();
                    MakeReport(book);
                    try { book.SaveAs(SavePath);
                        MessageBox.Show("報告書の出力が完了しました。");
                        SavePath = "";
                        }
                    catch { MessageBox.Show("保存に失敗しました。保存先のファイルを閉じてください"); }

                }
                else
                {
                    MessageBox.Show("選択された範囲内に代表事例はありませんでした。", "情報");
                }
            }
            else
            {
                MessageBox.Show(error);
            }
        }

        #endregion

        private void MakeReport(IXLWorkbook book)
        {
            //インデックスページの作成
            int Contentcounter = 0;
            int CellIndex = 4;
            IXLWorksheet sheet = book.AddWorksheet("Index");
            sheet.Range("B2", "C2").Merge().SetValue("感謝カード代表事例一覧（" + Term.Term1.ToShortDateString() + "～" + Term.Term2.ToShortDateString() + ")")
                .Style
                .Border.SetOutsideBorder(XLBorderStyleValues.Thin);

            sheet.Column("C").Width = 32.75;
            sheet.Column("B").Width = 18;

            sheet.Cell("B4").SetValue("項目番号").Style
                .Border.SetOutsideBorder(XLBorderStyleValues.Thick);

            sheet.Cell("C4").SetValue("タイトル").Style
                .Border.SetOutsideBorder(XLBorderStyleValues.Thick);

            foreach (ThanksCard t in ThanksCards)
            {
                CellIndex++;
                Contentcounter++;
                sheet.Cell("B" + CellIndex).Value = Contentcounter;
                sheet.Cell("C" + CellIndex).Value = t.Title;
                //代表事例一覧の作成
                MakeBody(book.AddWorksheet("代表事例" + Contentcounter), Term, t);
            }

            sheet.Range("B5", "C" + CellIndex).Style
                .Border.SetInsideBorder(XLBorderStyleValues.Thin)
                .Alignment.SetHorizontal(XLAlignmentHorizontalValues.Center);

            sheet.Range("B5","B"+CellIndex).Style
                .Border.SetOutsideBorder(XLBorderStyleValues.Thick);

            sheet.Range("C5", "C" + CellIndex).Style
                .Border.SetOutsideBorder(XLBorderStyleValues.Thick);

        }

        private void MakeBody(IXLWorksheet wt , Term term ,ThanksCard t)
        {
            wt.Range("B2", "I21").Style
                .Border.SetOutsideBorder(XLBorderStyleValues.Thin)
                .Fill.SetBackgroundColor(XLColor.LightSteelBlue);

            wt.Range("C3", "H3").Merge().Style
                .Alignment.SetHorizontal(XLAlignmentHorizontalValues.Center)
                .Border.SetOutsideBorder(XLBorderStyleValues.Thin)
                .Fill.SetBackgroundColor(XLColor.LightSlateGray);

            wt.Cell("C3").SetValue("感謝カード" + wt.Name + "（" + Term.Term1.ToShortDateString() + "～" + Term.Term2.ToShortDateString() + ")");

            wt.Range("E5", "G5").Merge().SetValue(t.From.Name)
                .Style
                .Border.SetOutsideBorder(XLBorderStyleValues.Thin)
                .Fill.SetBackgroundColor(XLColor.Lavender);

            wt.Range("E7", "G7").Merge().SetValue(t.To.Name).Style
                .Fill.SetBackgroundColor(XLColor.Lavender)
                .Border.SetOutsideBorder(XLBorderStyleValues.Thin);

            wt.Range("D9", "H9").Merge().SetValue(t.Title).Style
                .Fill.SetBackgroundColor(XLColor.Lavender)
                .Border.SetOutsideBorder(XLBorderStyleValues.Thin);

            wt.Cell("C12").SetValue(t.Body).Style
                .Alignment.SetVertical(XLAlignmentVerticalValues.Top)
                .Alignment.SetHorizontal(XLAlignmentHorizontalValues.Left);

            wt.Range("C12", "H20").Merge().Style
                .Fill.SetBackgroundColor(XLColor.Lavender)
                .Border.SetOutsideBorder(XLBorderStyleValues.Thin)
                .Alignment.SetWrapText(true);

            
            
            wt.Cell("D5").SetValue("From").Style
                .Border.SetOutsideBorder(XLBorderStyleValues.Thin)
                .Fill.SetBackgroundColor(XLColor.LightSlateGray);

            wt.Cell("D7").SetValue("To").Style
                .Border.SetOutsideBorder(XLBorderStyleValues.Thin)
                .Fill.SetBackgroundColor(XLColor.LightSlateGray);

            wt.Cell("C9").SetValue("タイトル").Style
                .Border.SetOutsideBorder(XLBorderStyleValues.Thin)
                .Fill.SetBackgroundColor(XLColor.LightSlateGray);

            wt.Cell("C11").SetValue("本文");

        }

        private string InputCheck()
        {
            string error = "";
            if (string.IsNullOrEmpty(SavePath))
            {
                error += "保存先を選択してください\n";
            }
            return error;
        }

        public void Initialize()
        {
            this.Term = new Term();
        }
    }
}
