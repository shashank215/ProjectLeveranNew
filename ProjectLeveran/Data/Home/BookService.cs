using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ProjectLeveran.Models;

namespace ProjectLeveran.Data.Home
{
    public class BookService
    {
        //public bool RequestCallBack(BookServiceViewModel model)
        //{
            
        //   string SQLText = "";
        //Dim db As Database = DatabaseFactory.CreateDatabase

        //SQLText.Append("update person set ")
        //SQLText.Append("preferred_contact_type_code = @aContactType, ")
        //SQLText.Append("transmitted_ind = 'N', ")
        //SQLText.Append("update_user_id = @aUserID, ")
        //SQLText.Append("Update_datetime = getdate() ")
        //SQLText.Append("where person_id = @aContactID ")

        //Dim cmd As DbCommand = db.GetSqlStringCommand(SQLText.ToString)
        //DBUtilities.AddInParameter(cmd, db, "aContactType", DbType.AnsiString, inputs(Schema.SI.Enums.Common.Contact.UpdatePreferredContactType.contactType))
        //DBUtilities.AddInParameter(cmd, db, "aUserID", DbType.AnsiString, inputs(Schema.SI.Enums.Common.Contact.UpdatePreferredContactType.currentUser))
        //DBUtilities.AddInParameter(cmd, db, "aContactID", DbType.AnsiString, inputs(Schema.SI.Enums.Common.Contact.UpdatePreferredContactType.personId))
        //DBUtilities.AddInParameter(cmd, db, "originalUpdateTimestamp", DbType.DateTime, inputs(Schema.SI.Enums.Common.Contact.UpdateContactInfo.originalUpdateTimestamp))

        //If transaction Is Nothing Then
        //    SM.IntegerValue = db.ExecuteNonQuery(cmd)
        //Else
        //    SM.IntegerValue = db.ExecuteNonQuery(cmd, CType(transaction, DbTransaction))
        //End If

        //}
    }
}