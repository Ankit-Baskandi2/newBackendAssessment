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

        //unsuccess Message
        public const string somethingWrongError = "Something went wrong";


        // Status Codes
        public const int successStatusCode = 200;
        public const int unsuccessStatusCode = 401;

        // store procedure

    }
}
