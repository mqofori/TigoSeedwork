using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.Security.Cryptography;

using ConsoleApplication3.UMarketService;
using System.ComponentModel;
using ConsoleApplication3.SoapClasses;

using SubscriberManagement.Domain.SMS;
using SubscriberManagement.Domain.Logger;


using System.Configuration;
using System.Xml;
using System.Xml.Linq;


using ConsoleApplication3.Model;
using ConsoleApplication3.Repository;
namespace ConsoleApplication3
{
    class Program
    {
        static void Main(string[] args)
        {
            #region temp
            
            //            int iii=1;

//            Console.WriteLine("Testing--Pos- re-processing");
//            IList<TempModel> new_request1 = new List<TempModel>(MainRepository.TempGetAllRequests());

//            foreach(var req in new_request1)
//            {
//                string ttt = SoapServices.SendXMLRequest(GetXMLCommands.GetRegisterPayment(req.phoneNumber,req.amount));

//                MainRepository.TempUpdateNumber(req);
//                Console.WriteLine(ttt);
//                Console.WriteLine(iii++);
//// testing 
            //            }
            #endregion
            Console.WriteLine("Tigo Cash Requests Count_v1.5");
            #region Constants
              string init = ConfigurationManager.AppSettings["init"].ToString();// "0277551990";//"0573449847";// "0278793422";
            string salt = ConfigurationManager.AppSettings["salt"].ToString(); //"0277551990";//"0278793422";
            string pin = ConfigurationManager.AppSettings["pin"].ToString(); // "1212";//"1234";
            string amount = ConfigurationManager.AppSettings["amount"].ToString(); // "1212";//"1234";
            string fundsource = ConfigurationManager.AppSettings["fundsource"].ToString(); // "1212";//"1234";
            #endregion
            
            int i=1;
            while (i > 0)
            {
                try
                {
                    IList<RequestModel> new_request = new List<RequestModel>(MainRepository.GetAllRequests());
                    int int_temp = 0;
                    foreach (RequestModel req in new_request)
                    {
                        if (MainRepository.OnWhileList(req))
                        {
                            MainRepository.UpdateTigoCashRequest(req, 3);

                            //SMSService.SendSMSMiddleware("Tigo", "0277443830", "Wc-request");

                            string session = CreatAndLoginSession();
                          //  int current_count = MainRepository.GetCurrentReceiptNumber() + 1;
                            string reponse = string.Empty;

                            // check if registered- to do; 
                            reponse = SoapServices.SendXMLRequest(GetXMLCommands.GetAgentStatusXML(req.SubscriberNumber, session));
                            

                            #region Registration        
                            if (reponse.Contains("cashsubscriber"))
                            {
                            }
                            else
                            {
                                reponse = SoapServices.SendXMLRequest(GetXMLCommands.GetRegisterXML(req.RequestBy, session, req.SubscriberNumber, req.SubscriberName, req.SubscriberAddress, "Ghanaian", req.DOB, req.SubscriberIdType, req.SubscriberIdNumber));
                                LogService.AutoLog(req.SubscriberNumber, DbFriendly(reponse), "TigoCashWelcomePack");
                                Console.WriteLine("--log--");
                            }
                            #endregion
                                                        
                            #region Activate     
                            for (int x=0; x<10; x++)
                            { 
                                
                                string receiptNumber = GetReceiptNumber();
                                reponse = SoapServices.SendXMLRequest(GetXMLCommands.GetActivateXML(session, req.SubscriberNumber, init, pin, receiptNumber));
                               // MainRepository.UpdateReceiptNumber(current_count);

                                LogService.AutoLog(req.SubscriberNumber, DbFriendly(reponse), "TigoCashWelcomePack");
                                Console.WriteLine("--log--");

                                if (CheckIfSucessfull(reponse))
                                {
                                    MainRepository.UpdateReceiptNumber(receiptNumber, 1);
                                    MainRepository.UpdateTigoCashRequest(req, 4);
                                    break;
                                }
                                else
                                {
                                    if (reponse.Contains("10006"))
                                    {
                                        MainRepository.UpdateTigoCashRequest(req, 5); // already active
                                        break;
                                    }
                                    else
                                        MainRepository.UpdateReceiptNumber(receiptNumber, 3); // already used
                                }
                                     
                                
                           
                            
                            }                         
                           
                            #endregion

                            #region Donate 1GHC     
                            
                            //if (MainRepository.CheckIfAlreadyProcessed(req.SubscriberNumber))
                            //{
                                //MainRepository.UpdateTigoCashRequest(req, 3); // duplicate request
                                //SMSService.SendSMSMiddleware("Tigo", req.RequestBy, "This subscriber " +  req.SubscriberIdNumber + "  has already received the tigo cash welcome package.");
                            //}
                            //else 
                            //{
                             //   reponse = SoapServices.SendXMLRequest(GetXMLCommands.GetAdjustWalletXML(session, fundsource,req.SubscriberNumber));
                               // LogService.AutoLog(req.SubscriberNumber, DbFriendly(reponse), "TigoCashWelcomePack");
                               #endregion

                            #region Check response                                
                            //if (CheckIfSucessfull(reponse))
                            //{
                            //    //SMSService.SendSMSMiddleware("Tigo", "0277443830", "WC-request-completed");
                            //    LogService.AutoLog(req.SubscriberNumber, "Operation Successful", "TigoCashWelcomePack");
                            //    MainRepository.UpdateTigoCashRequest(req, 1);
                            //    SMSService.SendSMSMiddleware("Tigo", req.SubscriberNumber, "Welcome to Tigo Cash, Your account has been credited with 1 GHC. Smile you've got Tigo");
                            //}
                            //else
                            //{
                            //    MainRepository.UpdateNumberOfTries(req);
                            //    SMSService.SendSMSMiddleware("Tigo", req.RequestBy, req.SubscriberNumber + ", Tigo cash registration was not successful");
                            //}
                            #endregion
                            //}

                            SMSService.SendSMSMiddleware("Tigo", req.SubscriberNumber, "Welcome to Tigo Cash, Your has been activated.");
                        }
                        else
                        {
                            MainRepository.UpdateTigoCashRequest(req, 2);
                            SMSService.SendSMSMiddleware("Tigo", req.RequestBy,"You are not permitted to undertake Tigo Cash transactions");
                        }
                        Console.WriteLine("Request No. " + int_temp++);
                    }
                }
                    catch (Exception ex)
                {
                    LogService.AutoLog("Application Error", ex.Message, "TigoCashWelcomePack");
                    Console.WriteLine("--error--");
                }
            }
        }


        #region Custom        
        /// <summary>
        /// Create a session and login to UMarket
        /// </summary>
        /// <returns></returns>
        static string CreatAndLoginSession()
        {
            #region Config            
            string init = ConfigurationManager.AppSettings["init"].ToString();// "0277551990";//"0573449847";// "0278793422";
            string salt = ConfigurationManager.AppSettings["salt"].ToString(); //"0277551990";//"0278793422";
            string pin = ConfigurationManager.AppSettings["pin"].ToString(); // "1212";//"1234";
            #endregion
            // Get 
            string response = SoapServices.SendXMLRequest(GetXMLCommands.GetSessionXML());
            var t = XElement.Parse(response);

            string session = t.Value.Remove(t.Value.Length - 8);
           
            pin = GeneralService.GetPin(session, salt, pin);
            response = GetXMLCommands.GetLoginXML(init, pin, session);
            response = SoapServices.SendXMLRequest(response);

            t = XElement.Parse(response);

            string transactionId = t.Value.Remove(t.Value.Length - 8);

            return session;
        }

        static bool CheckIfSucessfull(string response)
        {
            try
            {
                var t = XElement.Parse(response);
                int startPoing = t.Value.Length - 8;
                string check = t.Value.Substring(startPoing, 8);

                if (check == "0umarket")  
                    return true;
                else
                    return false;
            }
            catch { return false;  }
        }

        static string DbFriendly(string response)
        {
            try    
            {
                var t = XElement.Parse(response);
                string check = t.Value;
                return check;
            }
            catch { return "Non xml response"; }
        }

        static string GetReceiptNumber()
        {
            //string s = Guid.NewGuid().ToString();
            //s = s.Replace("-", "");            
                //Random r = new Random();
            //int rInt = r.Next(100000000, 100010000);
            //return s.Substring(0, 15);

            ReceiptModel receipt = MainRepository.GetReceipt();            

            return receipt.ReceiptNumber.ToString();
        }
        #endregion
    }

    //linq
}



