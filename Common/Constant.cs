namespace ForestInteractiveTestApp.Common
{
    public class Constant
    {
        #region ResponseMessages

        public const string SuccessResponse = "Success";
        public const string SuccessMessageResponse = "DELIVRD";
        public const string MessageSentResponse = "All the Messages has been sent";
        public const string ErrorMessageResponse = "Exception occured, Please Debug the specific Method";

        #endregion

        #region Store Procedures

        public const string SpGetSchedulesList = "usp_GetSchedulesList";
        public const string SpGetSMSScheduleList = "usp_GetSMSScheduleList";
        public const string SpCreateUpdateSchedule = "usp_CreateUpdateSchedule";

        #endregion

        #region Enumeration

        public enum TestType
        {
            ScheduleTest = 1,
        }

        #endregion
    }
}
