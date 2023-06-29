using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using RadarFII.Business.Interfaces.Service;
using RadarFII.Business.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RadarFII.Service.RabbitMQ
{
    public class RabbitMQService : IRabbitMQService
    {
        public void Publicar(ProventoFII proventoFII)
        {

            var factory = new ConnectionFactory() { HostName = "localhost", UserName= "guest", Password = "guest" };
                Password = "RadarFIIingestao07#" };

            using (var connection = factory.CreateConnection())
            using (var channel = connection.CreateModel())
            {
                channel.ConfirmSelect();
                channel.BasicAcks += Evento_Confirmacao;
                channel.BasicNacks += Evento_NaoConfirmacao;

                channel.QueueDeclare(queue: "proventosFII",
                                     durable: false,
                                     exclusive: false,
                                     autoDelete: false,
                                     arguments: null);

                var json = Newtonsoft.Json.JsonConvert.SerializeObject(proventoFII);

                var body = Encoding.UTF8.GetBytes(json);

                channel.BasicPublish(exchange: "",
                                             routingKey: "proventosFII",
                                             basicProperties: null,
                                             body: body);

                Console.WriteLine("Mensagem enviada!");
            }
        }

        private void Evento_NaoConfirmacao(object sender, BasicNackEventArgs e)
        {
            Console.WriteLine("Nack");
        }

        private void Evento_Confirmacao(object sender, BasicAckEventArgs e)
        {
            Console.WriteLine("Ack");
        }
    }
}

