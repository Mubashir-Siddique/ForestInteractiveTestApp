using Newtonsoft.Json;
using System;
using System.Linq;
using ForestInteractiveTestApp.Common;
using ForestInteractiveTestApp.IRepository;
using ForestInteractiveTestApp.Models;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json.Linq;

namespace ForestInteractiveTestApp.Core
{
    public class ScheduleCore
    {
        private IRepository<Schedule> _iRepository;

        APIResponse apiResponse = new APIResponse();

        public ScheduleCore(IRepository<Schedule> iRepository)
        {
            _iRepository = iRepository;
        }
        public APIResponse GetSchedulesList(ScheduleRequestModel model)
        {
            apiResponse.Response = _iRepository.Search<Schedule>(model, Constant.SpGetSchedulesList).ToList();
            return apiResponse;
        }
        public APIResponse GetSMSScheduleList(Schedule model)
        {
            apiResponse.Response = _iRepository.Search<Schedule>(model, Constant.SpGetSMSScheduleList).ToList();
            return apiResponse;
        }

        public APIResponse CreateUpdateSchedule(Schedule model)
        {
            var obj = new {
                ScheduleId = model.ScheduleId,
                Time = model.Time,
                Recepients = JsonConvert.SerializeObject(model.Recepients),
                Message = model.Message,
                IsDone = false,
                IsActive = model.IsActive,
                InsertedOn = DateTime.Now,
                UpdatedOn = DateTime.Now,
            };

            Schedule ResponseObj = _iRepository.Search<Schedule>(obj, Constant.SpCreateUpdateSchedule).FirstOrDefault();

            ResponseObj.Recepients = JsonConvert.DeserializeObject<List<Recepient>>(ResponseObj.RecepientList);
            ResponseObj.RecepientList = string.Empty;

            apiResponse.Response = ResponseObj;
            return apiResponse;
        }

        public async Task<APIResponse> SendScheduleSMS(DateTime time)
        {
            List<ApiResponseContent> responseList = new List<ApiResponseContent>();

            try
            {
                var model = new { Status = true, FromDate = time, ToDate = time.AddMinutes(1) };

                var ScheduleList = _iRepository.Search<Schedule>(model, Constant.SpGetSchedulesList).ToList();

                if (ScheduleList.Count > 0)
                {
                    foreach (var recepient in ScheduleList)
                    {
                        var RecepientList = JsonConvert.DeserializeObject<List<Recepient>>(recepient.RecepientList);

                        var PhoneNumList = (from r in RecepientList
                                            select r.PhoneNumber).ToList();

                        string ContactData = string.Join(",", PhoneNumList);

                        using (HttpClient client = new HttpClient())
                        {
                            client.BaseAddress = new Uri("http://kr8tif.lawaapp.com:1338");

                            var PostData = new FormUrlEncodedContent(new[]
                            {
                            new KeyValuePair<string, string>("dnis", ContactData),
                            new KeyValuePair<string, string>("message", "Test Message From Mubashir")
                        });

                            var result = await client.PostAsync("/api", PostData);
                            string resultContent = await result.Content.ReadAsStringAsync();

                            responseList = JsonConvert.DeserializeObject<List<ApiResponseContent>>(resultContent);

                            foreach (var recepObj in RecepientList)
                            {
                                var x = from obj in responseList
                                        where obj.dnis.Equals(recepObj.PhoneNumber)
                                        select obj;
                                recepObj.MessageId = x.FirstOrDefault().message_id;
                            }

                            recepient.Recepients = RecepientList;

                            var modelObj = new
                            {
                                ScheduleId = recepient.ScheduleId,
                                Time = recepient.Time,
                                Recepients = JsonConvert.SerializeObject(RecepientList),
                                Message = recepient.Message,
                                IsDone = false,
                                IsActive = recepient.IsActive,
                                InsertedOn = recepient.InsertedOn,
                                UpdatedOn = DateTime.Now,
                            };

                            _iRepository.Search<Schedule>(modelObj, Constant.SpCreateUpdateSchedule).FirstOrDefault();
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                apiResponse.Response = Constant.ErrorMessageResponse + ex.Message + " " + ex.StackTrace;
                apiResponse.StatusCode = APIResponseCodes.StatusCodeERROR;
                return apiResponse;
            }

            apiResponse.Response = Constant.MessageSentResponse;
            return apiResponse;
        }

        public async Task<APIResponse> CheckSMSStatus(string messageId)
        {
            try
            {
                using (HttpClient client = new HttpClient())
                {
                    client.BaseAddress = new Uri("http://kr8tif.lawaapp.com:1338");

                    var result = await client.GetStringAsync("/api?messageId=" + messageId);

                    var data = JObject.Parse(result);

                    if (data["status"].Equals(Constant.SuccessMessageResponse))
                    {
                        var recepientList = _iRepository.Search<Schedule>(new { }, Constant.SpGetSchedulesList).ToList();
                    }
                }
            }
            catch (Exception ex)
            {
                apiResponse.Response = Constant.ErrorMessageResponse + ex.Message + " " + ex.StackTrace;
                apiResponse.StatusCode = APIResponseCodes.StatusCodeERROR;
                return apiResponse;
            }

            apiResponse.Response = Constant.SuccessResponse;
            return apiResponse;
        }
    }
}
