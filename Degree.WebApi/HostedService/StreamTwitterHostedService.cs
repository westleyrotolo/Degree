using Degree.Models;
using Degree.Models.Twitter;
using Degree.Services.Social.Twitter;
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
    public class StreamTwitterHostedService : Hub, IHostedService
    {


        private readonly Dictionary<string, TwitterApi> Connections; 
        private readonly IHubContext<StreamTwitterHostedService> _hubContext;
        Services.Social.Twitter.TwitterApi TwitterApi = new Services.Social.Twitter.TwitterApi();
        public StreamTwitterHostedService(IHubContext<StreamTwitterHostedService> hubContext)
        {
            _hubContext = hubContext;
            Connections = new Dictionary<string, TwitterApi>();

        }
        public Task StartAsync(CancellationToken cancellationToken)
        {
            

            Task.Run(()=>
            {
                
            });
            return Task.CompletedTask;

        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            return Task.CompletedTask;
        }


        public void Update(Degree.Models.Dto.TweetDto tweet, string Connection)
        {
            _hubContext.Clients.Client(Connection).SendAsync("broadcastMessage", tweet);
        }
        [HubMethodName("query")]
        public async Task ConnectClient(QueryStream query)
        {
            var connection = Context.ConnectionId;
            if (!Connections.ContainsKey(connection))
            {
                var twitterApi = new TwitterApi();
                twitterApi.StreamTwitter(Update, connection, query.Tracks, false);
                Connections.Add(connection, twitterApi);
            }
            else
            {
                Connections[connection].StopStream();
                Connections[connection].StreamTwitter(Update, connection, query.Tracks, false);
            }
        }
        public override Task OnConnectedAsync()
        {
            return base.OnConnectedAsync();
        }
        public override Task OnDisconnectedAsync(Exception exception)
        {
            return base.OnDisconnectedAsync(exception);
        }
    }
    public class QueryStream
    {
        public List<string> Tracks { get; set; }
        public bool GeoEnabled { get; set; }
    }
}
