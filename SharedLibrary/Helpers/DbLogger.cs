using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Npgsql;
using SharedLibrary.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SharedLibrary.Helpers
{
    public class DbLogger : ILogger
    {
        /// <summary>
        /// Instance of <see cref="DbLoggerProvider" />.
        /// </summary>
        private readonly DbLoggerProvider _dbLoggerProvider;

        /// <summary>
        /// Creates a new instance of <see cref="FileLogger" />.
        /// </summary>
        /// <param name="fileLoggerProvider">Instance of <see cref="FileLoggerProvider" />.</param>
        public DbLogger([NotNull] DbLoggerProvider dbLoggerProvider)
        {
            _dbLoggerProvider = dbLoggerProvider;
        }

        public IDisposable BeginScope<TState>(TState state)
        {
            return null;
        }

        /// <summary>
        /// Whether to log the entry.
        /// </summary>
        /// <param name="logLevel"></param>
        /// <returns></returns>
        public bool IsEnabled(LogLevel logLevel)
        {
            return logLevel != LogLevel.None;
        }


        /// <summary>
        /// Used to log the entry.
        /// </summary>
        /// <typeparam name="TState"></typeparam>
        /// <param name="logLevel">An instance of <see cref="LogLevel"/>.</param>
        /// <param name="eventId">The event's ID. An instance of <see cref="EventId"/>.</param>
        /// <param name="state">The event's state.</param>
        /// <param name="exception">The event's exception. An instance of <see cref="Exception" /></param>
        /// <param name="formatter">A delegate that formats </param>
        public void Log<TState>(LogLevel logLevel, EventId eventId, TState state, Exception exception, Func<TState, Exception, string> formatter)
        {
            if (!IsEnabled(logLevel))
            {
                return;
            }
            InfoDetail info = null;
            try
            {
                info = InfoDetail.GetFromString(formatter(state, exception));
                
            }
            catch (Exception ex)
            {
                Console.WriteLine(formatter(state, exception));
                return;
            }

            if (logLevel==LogLevel.Information)
            {
                LogInformation(eventId, info);
                return;
            }


            var threadId = Thread.CurrentThread.ManagedThreadId; // Get the current thread ID to use in the log file. 

            // Store record.
            using (NpgsqlConnection connection = new NpgsqlConnection(_dbLoggerProvider.Options.ConnectionString))
            {
                connection.Open();

                // Add to database.

                // LogLevel
                // ThreadId
                // EventId
                // Exception Message (use formatter)
                // Exception Stack Trace
                // Exception Source

                var values = new JObject();

                if (!string.IsNullOrWhiteSpace(logLevel.ToString()))
                {
                    values["LogLevel"] = logLevel.ToString();
                }
                values["ThreadId"] = threadId;
                values["EventId"] = eventId.Id;
                if (!string.IsNullOrWhiteSpace(eventId.Name))
                {
                    values["EventName"] = eventId.Name;
                }
                if (!string.IsNullOrWhiteSpace(formatter(state, exception)))
                {
                    values["Message"] = formatter(state, exception);
                }
                if (exception != null && !string.IsNullOrWhiteSpace(exception.Message))
                {
                    values["ExceptionMessage"] = exception?.Message;
                }
                if (exception != null && !string.IsNullOrWhiteSpace(exception.StackTrace))
                {
                    values["ExceptionStackTrace"] = exception?.StackTrace;
                }
                if (exception != null && !string.IsNullOrWhiteSpace(exception.Source))
                {
                    values["ExceptionSource"] = exception?.Source;
                }


                using (var command = new NpgsqlCommand())
                {
                    command.Connection = connection;
                    command.CommandType = System.Data.CommandType.Text;
                    command.CommandText =
                        string.Format(
                            "INSERT INTO" +
                            " \"{0}\" (\"Ip\", \"DeviceId\", \"EventId\", \"EventName\" , \"UserId\", \"DeviceType\" , \"DeviceModel\", \"OsVersion\" , \"AppVersion\" , \"AdditionalInfo\", \"Created\" , \"Duration\" , \"Action\" , \"Body\" ,\"Exception\" , \"InnerException\" )" +
                            " VALUES " +
                            "(@Ip, @DeviceId, @EventId, @EventName, @UserId, @DeviceType , @DeviceModel, @OsVersion , @AppVersion , @AdditionalInfo, @Created, @Duration, @Action, @Body , @Exception, @InnerException)"
                            , _dbLoggerProvider.Options.LogTable
                            );

                    command.Parameters.Add(new NpgsqlParameter("@Ip", info.Ip));
                    command.Parameters.Add(new NpgsqlParameter("@DeviceId", info.DeviceId));
                    command.Parameters.Add(new NpgsqlParameter("@EventId", eventId.Id));
                    command.Parameters.Add(new NpgsqlParameter("@EventName", eventId.Name ?? ""));
                    command.Parameters.Add(new NpgsqlParameter("@UserId", info.UserId));
                    command.Parameters.Add(new NpgsqlParameter("@DeviceType", info.DeviceType));
                    command.Parameters.Add(new NpgsqlParameter("@DeviceModel", info.DeviceModel));
                    command.Parameters.Add(new NpgsqlParameter("@OsVersion", info.OsVersion));
                    command.Parameters.Add(new NpgsqlParameter("@AppVersion", info.AppVersion));
                    command.Parameters.Add(new NpgsqlParameter("@Created", info.Created));
                    command.Parameters.Add(new NpgsqlParameter("@Duration", (DateTimeOffset.Now - info.Created).GetValueOrDefault().TotalMilliseconds));
                    command.Parameters.Add(new NpgsqlParameter("@AdditionalInfo", info.AdditionalInfo));
                    command.Parameters.Add(new NpgsqlParameter("@Action", info.Action));
                    command.Parameters.Add(new NpgsqlParameter("@Body", info.Body));
                    command.Parameters.Add(new NpgsqlParameter("@Exception", info.Exception));
                    command.Parameters.Add(new NpgsqlParameter("@InnerException", info.InnerException));

                    command.ExecuteNonQuery();
                }

                connection.Close();
            }
        }

        public void LogInformation(EventId eventId,InfoDetail info)
        {
            // Store record.
            try
            {
                using (NpgsqlConnection connection = new NpgsqlConnection(_dbLoggerProvider.Options.ConnectionString))
                {
                    connection.Open();

                    // Add to database.

                    // LogLevel
                    // ThreadId
                    // EventId
                    // Exception Message (use formatter)
                    // Exception Stack Trace
                    // Exception Source

                    var values = new JObject();
                    values["EventId"] = eventId.Id;
                    if (!string.IsNullOrWhiteSpace(eventId.Name))
                    {
                        values["EventName"] = eventId.Name;
                    }


                    using (var command = new NpgsqlCommand())
                    {
                        command.Connection = connection;
                        command.CommandType = System.Data.CommandType.Text;
                        command.CommandText =
                            string.Format(
                                "INSERT INTO"+
                                " \"{0}\" (\"Ip\", \"DeviceId\", \"EventId\", \"EventName\" , \"UserId\", \"DeviceType\" , \"DeviceModel\", \"OsVersion\" , \"AppVersion\" , \"AdditionalInfo\", \"Created\" , \"Duration\" , \"Action\" , \"Body\" )" +
                                " VALUES "+
                                "(@Ip, @DeviceId, @EventId, @EventName, @UserId, @DeviceType , @DeviceModel, @OsVersion , @AppVersion , @AdditionalInfo, @Created, @Duration, @Action, @Body)"
                                , _dbLoggerProvider.Options.LogInfoTable
                                );

                        command.Parameters.Add(new NpgsqlParameter("@Ip", info.Ip));
                        command.Parameters.Add(new NpgsqlParameter("@DeviceId", info.DeviceId));
                        command.Parameters.Add(new NpgsqlParameter("@EventId", eventId.Id));
                        command.Parameters.Add(new NpgsqlParameter("@EventName", eventId.Name??""));
                        command.Parameters.Add(new NpgsqlParameter("@UserId", info.UserId));
                        command.Parameters.Add(new NpgsqlParameter("@DeviceType", info.DeviceType));
                        command.Parameters.Add(new NpgsqlParameter("@DeviceModel", info.DeviceModel));
                        command.Parameters.Add(new NpgsqlParameter("@OsVersion", info.OsVersion));
                        command.Parameters.Add(new NpgsqlParameter("@AppVersion", info.AppVersion));
                        command.Parameters.Add(new NpgsqlParameter("@Created", info.Created));
                        command.Parameters.Add(new NpgsqlParameter("@Duration", (DateTimeOffset.Now - info.Created).GetValueOrDefault().TotalMilliseconds));
                        command.Parameters.Add(new NpgsqlParameter("@AdditionalInfo", info.AdditionalInfo));
                        command.Parameters.Add(new NpgsqlParameter("@Action", info.Action));
                        command.Parameters.Add(new NpgsqlParameter("@Body", info.Body));

                        command.ExecuteNonQuery();
                    }

                    connection.Close();
                }
            }
            catch (Exception e)
            {

                throw e;
            }
        }
    }
}
