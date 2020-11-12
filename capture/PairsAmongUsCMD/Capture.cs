using System;
using System.Collections.Generic;
using HamsterCheese.AmongUsMemory;
using System.Threading;
using System.Threading.Tasks;
using System.IO;
using NATS.Client;
using System.Text;

namespace PairsAmongUsCMD
{
    class Capture
    {
        Dictionary<byte, Player> players = new Dictionary<byte, Player>();
        List<PlayerData> playerDatas = new List<PlayerData>();
        ConnectionFactory natsConnectionFactory = new ConnectionFactory();
        IConnection natsConnection;
        void UpdateCheat()
        {
            while (true)
            {
                foreach (var data in playerDatas)
                {
                    var player = players[data.PlayerInfo.Value.PlayerId];

                    player.Name = Utils.ReadString(data.PlayerInfo.Value.PlayerName);
                    player.IsImposter = Convert.ToBoolean(data.PlayerInfo.Value.IsImpostor);
                    player.IsDead = Convert.ToBoolean(data.PlayerInfo.Value.IsDead);
                    player.Position = data.Position;
                    var json = Newtonsoft.Json.JsonConvert.SerializeObject(player);
                    natsConnection.Publish("data", Encoding.UTF8.GetBytes(json));
                }
                System.Threading.Thread.Sleep(100);
            }
        }

        public void Init()
        {
            Console.SetOut(TextWriter.Null); // Suppress library output
            natsConnection = natsConnectionFactory.CreateConnection("nats://localhost:4222");

            // Cheat Init
            if (HamsterCheese.AmongUsMemory.Cheese.Init())
            {
                // Update Player Data When Join New Map.
                HamsterCheese.AmongUsMemory.Cheese.ObserveShipStatus(OnDetectJoinNewGame);

                // Start Your Cheat 
                CancellationTokenSource cts = new CancellationTokenSource();
                Task.Factory.StartNew(
                    UpdateCheat
                , cts.Token);
            }
        }

        void OnDetectJoinNewGame(uint obj)
        {
            playerDatas = HamsterCheese.AmongUsMemory.Cheese.GetAllPlayers();
            players.Clear();
            foreach (var data in playerDatas)
            {
                players.Add(data.PlayerInfo.Value.PlayerId, new Player());
            }
        }
    }
}
