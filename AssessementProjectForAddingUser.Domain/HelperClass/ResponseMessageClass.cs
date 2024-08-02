using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AssessementProjectForAddingUser.Domain.HelperClass
{
    public static class ResponseMessageClass
    {
        //Success Message
        public const string loginSuccess = "Login successfully";
        public const string loginError = "Login unsuccess";
        public const string addedSuccess = "Data saved successfully";
        public const string invalidUser = "Your are not a registered user";
        public const string updateSuccess = "Data updated successfully";
        public const string notFound = "Data not found";
        public const string oldPasswordIncorrect = "Your old password is incorrect";
        public const string emptyMessage = "";
        public const string emailSuccess = "Email sent successfully";
        public const string tokenExpired = "Your token is expired please login again";
        public const string unauthorizeUser = "Unauthorized User";

        //unsuccess Message
        public const string somethingWrongError = "Something went wrong";


        // Status Codes
        public const int successStatusCode = 200;
        public const int unsuccessStatusCode = 401;
        public const int notFoundStatusCode = 404;
        public const int badRequestStatusCode = 500;

        // store procedure

    }
}
