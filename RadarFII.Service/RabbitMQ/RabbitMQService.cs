using Amazon;
using Amazon.MQ;
using Amazon.MQ.Model;
using Amazon.Runtime;
using Amazon.SQS;
using Amazon.SQS.Model;
using Newtonsoft.Json;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using RadarFII.Business.Interfaces.Service;
using RadarFII.Business.Models;
using RadarFII.Service.Models;
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

            // Specify your AWS access key and secret key
            string awsAccessKeyId = "***";
            string secretKey = "**+o";

            string message = JsonConvert.SerializeObject(proventoFII);
            // Specify the message you want to publish

            try
            {
                var credentials = new BasicAWSCredentials(awsAccessKeyId, secretKey);
                var sqsConfig = new AmazonSQSConfig
                {
                    RegionEndpoint = RegionEndpoint.SAEast1
                };

                using (var sqsClient = new AmazonSQSClient(credentials, sqsConfig))
                {
                    var sendMessageRequest = new SendMessageRequest
                    {
                        QueueUrl = "https://sqs.sa-east-1.amazonaws.com/128601947047/RadarFIIingestao",
                        MessageBody = message
                    };

                    var sendMessageResponse = sqsClient.SendMessageAsync(sendMessageRequest).GetAwaiter().GetResult();

                    Console.WriteLine("Message sent successfully!");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error: " + ex.Message);
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

