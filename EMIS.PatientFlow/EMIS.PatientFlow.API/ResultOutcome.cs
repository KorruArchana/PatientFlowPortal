using System.Collections.Generic;
using System.Linq;

namespace EMIS.PatientFlow.API
{   
    internal class ResultOutcome
    {
        private static readonly ResultOutcome _outcomeList = new ResultOutcome();

         private readonly List<List<string>> _messageList = new List<List<string>>();
         public string[] this[int index]
         {
             get
             {
                 if (index >= 0 && index <= _messageList.Count - 1)
                 {
                     return _messageList.ElementAt(index).ToArray();
                 }
                 else
                 {
                     return null;
                 }
             }
         }

         private ResultOutcome()
         {
             _messageList.Add(
                 (new[]
                      {
                          "Refer to error", "Successful initialise awaiting logon",
                          "Unable to connect to server due to absent server, or incorrect details", "Unmatched SupplierID",
                          "Autologon successful"
                      }).ToList());

             _messageList.Add(
                 (new[]
                      {
                          "Technical error", "Successful", "Expired", "unsuccessful",
                          "Invalid login ID or login ID does not have access to this product"
                      }).ToList());

             _messageList.Add((new[] { "Technical error", "Successful", "Access denied", "No patients" }).ToList());
             _messageList.Add((new[] { "Technical error", "Successful", "Access denied", "No patients" }).ToList());
             _messageList.Add((new[] { "Technical error", "Successful", "Access denied", "Nothing to return" }).ToList());
             _messageList.Add((new[] { "Technical error", "Successful", "Access denied" }).ToList());
             _messageList.Add((new[] { "Technical error", "Successful", "Access denied" }).ToList());
             _messageList.Add(
                 (new[]
                      {
                          "Technical error", "Successful", "Access denied", "Status already set",
                          "Incompatible with current status", "Slot is not booked"
                      }).ToList());
             _messageList.Add((new[] { "Technical error", "Access allowed", "Access denied" }).ToList());
             _messageList.Add(
                 (new[]
                      {
                          "Technical error", "Successful", "Access denied", "Technical error (other)", "Not logged in" 
                      })
                     .ToList());
             _messageList.Add((new[] { "Technical error", "Successful", "Access denied" }).ToList());
             _messageList.Add((new[] { "Technical error", "Access allowed", "Access denied" }).ToList());
			_messageList.Add((new[] { "Technical error", "Successful", "Access denied", "Nothing to return" }).ToList());
		}

        public static ResultOutcome MessageList
        {
            get
            {
                return _outcomeList;
            }
        }
    }
}
