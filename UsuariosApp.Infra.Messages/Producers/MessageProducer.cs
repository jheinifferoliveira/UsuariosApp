using RabbitMQ.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UsuariosApp.Domain.Interfaces.Messages;
using UsuariosApp.Infra.Messages.Settings;

namespace UsuariosApp.Infra.Messages.Producers
{
    public class MessageProducer: IMessageProducer
    {
        public void Send(string message)
        {
            #region Conectando no servidor do RabbitMQ

            var connectionFactory = new ConnectionFactory
            {
                Uri=new Uri(RabbitMQSettings.RabbiUrl)
            };

            #endregion

            #region Gravando a mensagem na fila

            using (var connection = connectionFactory.CreateConnection())
            {
                using (var model = connection.CreateModel())
                {
                    model.QueueDeclare(
                        queue: RabbitMQSettings.RabbitQueue, 
                        durable: true, 
                        exclusive: false, 
                        autoDelete: false, 
                        arguments: null
                        );

                 
                    model.BasicPublish(
                        exchange: string.Empty,
                        routingKey: RabbitMQSettings.RabbitQueue,
                        basicProperties: null,
                        body: Encoding.UTF8.GetBytes(message)
                        );
                }
            }


            #endregion
        }
    }
}
