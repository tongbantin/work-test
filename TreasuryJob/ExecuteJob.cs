using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Quartz;
using Quartz.Impl; 
using log4net;
using System.Configuration;
using TreasuryJob.Batch;

namespace TreasuryJob
{
    public class ExecuteJob
    {
        private static ILog Log = log4net.LogManager.GetLogger(typeof(ExecuteJob));
       
        public static void Start()
        {
            try
            {
                IScheduler scheduler = StdSchedulerFactory.GetDefaultScheduler();
                scheduler.Start();


                //List<BatchSchedule> batch = BatchSchedule.GetBatchSchedule();
                //for (int i = 0; i < batch.Count(); i++)
                //{
                    IJobDetail jobdetail1 = JobBuilder.Create<BatchSWIFTFileOutgoing>().Build();             

                    //string[] strtime = batch[i].SCHEDULETIME.Split(':');

                    ITrigger trigger1 = TriggerBuilder.Create()
                                        .WithIdentity("Trigger1", "Group1")
                                        .StartNow()
                                        .WithSimpleSchedule(x => x
                                            .WithIntervalInSeconds(420)
                                            .RepeatForever()
                                        )
                                        //.WithSchedule(CronScheduleBuilder.DailyAtHourAndMinute(int.Parse(strtime[0]), int.Parse(strtime[1])))
                                        .Build();

                    scheduler.ScheduleJob(jobdetail1, trigger1);

                //}
                 
            }
            catch (Exception ex)
            {
                Log.Error(ex.Message);
            }
        }
    }
}
