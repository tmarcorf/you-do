namespace YouDo.Core.Validations
{
    public static class ToDoValidationMessages
    {
        public const string INVALID_ID = "Invalid Id. Id is required.";

        public const string INVALID_USER_ID = "Invalid userId. userId is required.";

        public const string INVALID_TITLE = "Invalid title. Title is required.";

        public const string INVALID_TITLE_LENGTH = "Too short title. Mininum 5 characters is required.";

        public const string INVALID_TITLE_MAX_LENGTH = "Too long title. Maximum 100 characters is required.";

        public const string INVALID_DETAILS_MAX_LENGTH = "Too long details. Maximum 500 characters is required.";
    }
}
