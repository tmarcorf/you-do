﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YouDo.Core.Validations
{
    public static class ToDoValidationMessages
    {
        public const string INVALID_ID = "Invalid Id. Id is required.";

        public const string INVALID_TITLE = "Invalid title. Title is required.";

        public const string INVALID_TITLE_LENGTH = "Too short title. Mininum 5 characters is required.";
    }
}