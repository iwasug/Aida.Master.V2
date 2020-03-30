using AIDA.Master.Infrastucture.Constants;
using AIDA.Master.Infrastucture.Data;
using AIDA.Master.Service.Localizations;
using AIDA.Master.Service.Models;
using NPOI.SS.UserModel;
using Radyalabs.Core.Repository;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Web;

namespace AIDA.Master.Service.Businesses
{
    public class CollectionBusiness : BaseBusiness
    {
        public AlertMessage ImportCollection(ImportCollectionModel model)
        {
            AlertMessage alert = new AlertMessage();

            //if (!IsAccessible(ModuleCode.ImportCollection))
            //{
            //    alert.Text = StaticMessage.ERR_ACCESS_DENIED;
            //    return alert;
            //}

            int month = 0;
            int year = 0;

            try
            {
                string[] arr = model.FormattedValidDate.Split('-');

                month = Convert.ToInt16(arr[0]);
                year = Convert.ToInt16(arr[1]);
                List<UploadCollectionModel> list = GetDataCollection(model.InputFile, year, month);
                if(list != null)
                {
                    if(list.Count != 0)
                    {
                        IRepository<OpenBalanceMonthly> repoOpenBalanceMonthly = _unitOfWork.GetRepository<OpenBalanceMonthly>();
                        //_unitOfWork.BeginTransaction();
                        StringBuilder sb = new StringBuilder();
                        foreach (var item in list)
                        {
                            sb.Append($"INSERT INTO OpenBalanceMonthly VALUES ({item.TAHUN},{item.BULAN},{item.PLANT},'{item.REFERENCE}',{item.CUSTOMER},'{item.DUEDATE.ToString("yyyy-MM-dd")}','{item.CG1}','{item.PH3}','{item.MATERIAL}',{item.AMOUNT_09},'{item.INTERV}') \n");
                            //OpenBalanceMonthly openBalanceMonthly = new OpenBalanceMonthly()
                            //{
                            //    AMOUNT_09 = item.AMOUNT_09,
                            //    INTERV = item.INTERV,
                            //    TAHUN = item.TAHUN,
                            //    BULAN = item.BULAN,
                            //    CG1 = item.CG1,
                            //    CUSTOMER = item.CUSTOMER,
                            //    REFERENCE = item.REFERENCE,
                            //    PLANT = item.PLANT,
                            //    PH3 = item.PH3,
                            //    MATERIAL = item.MATERIAL,
                            //    DUEDATE = item.DUEDATE
                            //};
                            //repoOpenBalanceMonthly.Insert(openBalanceMonthly);
                        }
                        //_logger.Write("SQL", DateTime.Now, sb.ToString());
                        //_unitOfWork.Commit();
                    }
                }
            }
            catch (Exception ex)
            {
                alert.Text = StaticMessage.ERR_INVALID_INPUT;
                return alert;
            }

            return alert;
        }

        public List<UploadCollectionModel> GetDataCollection(HttpPostedFileBase postedFile, int year, int month)
        {
            List<UploadCollectionModel> result = new List<UploadCollectionModel>();
            Regex regex = new Regex(@"\u0009");
            StreamReader file = new StreamReader(postedFile.InputStream);
            string line;
            while ((line = file.ReadLine()) != null)
            {
                if (line.Contains(" "))
                {
                    string[] lineArray = regex.Replace(line, "#").Split('#');
                    if (lineArray[1].Trim() != "BP")
                    {
                        try
                        {
                            result.Add(new UploadCollectionModel()
                            {
                                TAHUN = year,
                                BULAN = month,
                                PLANT = int.Parse(lineArray[1].Trim()),
                                CUSTOMER = lineArray[2].Trim(),
                                INTERV = lineArray[4].Trim(),
                                MATERIAL = lineArray[7].Trim(),
                                CG1 = lineArray[5].Trim(),
                                PH3 = lineArray[6].Trim(),
                                REFERENCE = lineArray[8].Trim(),
                                DUEDATE = DateTime.Parse(lineArray[12].Trim().Substring(6, 4) + "-" + lineArray[12].Trim().Substring(3, 2) + "-" + lineArray[12].Trim().Substring(0, 2)),
                                AMOUNT_09 = decimal.Parse(lineArray[13].Trim())
                            });
                        }
                        catch (Exception ex)
                        {
                            //_logger.Write("ERROR", DateTime.Now, ex.Message + "\n" + line);
                        }
                        
                    }
                }
            }
            return result;
        }
    }
}
