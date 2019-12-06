using Degree.Models.Twitter;
using Degree.Services.Utils;
using Degree.WebApi.Controllers;
using Microsoft.AspNetCore.SignalR;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Degree.WebApi.HostedService
{
    public class StreamTwitterHostedService : IHostedService
    {
        private readonly IHubContext<NotifyHub> _hubContext;
        Services.Social.Twitter.TwitterApi TwitterApi = new Services.Social.Twitter.TwitterApi();
        public StreamTwitterHostedService(IHubContext<NotifyHub> hubContext)
        {
            _hubContext = hubContext;
        }
        public Task StartAsync(CancellationToken cancellationToken)
        {

            Task.Run(()=>
            {
                TwitterApi.StreamTwitter(Update);
                
            });
            return Task.CompletedTask;

        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            TwitterApi.StopStream();
            return Task.CompletedTask;
        }

        public void Update(Degree.Models.TweetRaw tweet)
        {
            _hubContext.Clients.All.SendAsync("broadcastMessage", tweet);
        }
    }
}
