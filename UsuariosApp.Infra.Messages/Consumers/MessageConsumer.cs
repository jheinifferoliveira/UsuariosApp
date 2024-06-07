using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Newtonsoft.Json;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using UsuariosApp.Domain.Models;
using UsuariosApp.Infra.Logs.Collections;
using UsuariosApp.Infra.Logs.Persistence;
using UsuariosApp.Infra.Messages.Helpers;
using UsuariosApp.Infra.Messages.Settings;

namespace UsuariosApp.Infra.Messages.Consumers
{
    public class MessageConsumer:BackgroundService
    {
        #region Atributos

        private readonly IServiceProvider? _serviceProvider;
        private readonly IConnection? _connection;
        private readonly IModel? _model;

        #endregion

        #region Construtor

        public MessageConsumer(IServiceProvider? serviceProvider)
        {
            _serviceProvider = serviceProvider;

            var connectionFactory= new ConnectionFactory { Uri = new Uri(RabbitMQSettings.RabbiUrl) };

          
            _connection = connectionFactory.CreateConnection();
            _model = _connection.CreateModel();
            _model?.QueueDeclare(
                queue: RabbitMQSettings.RabbitQueue,
                durable: true,
                exclusive: false,
                autoDelete: false,
                arguments: null
                );
        }

        #endregion


    protected override Task ExecuteAsync(CancellationToken stoppingToken)
        {
            var consumer = new EventingBasicConsumer(_model);

            consumer.Received += (send, args) =>
            {
              
                var contentArray = args.Body.ToArray();
                var contentString = Encoding.UTF8.GetString(contentArray);

              
                var mensagem = JsonConvert.DeserializeObject<Mensagem>(contentString);

              
                using (var scope = _serviceProvider.CreateScope())
                {
                    var logMensagens = new LogMensagens()
                    {
                        Id=Guid.NewGuid(),
                        DataHora=DateTime.Now

                    };
                    try
                    {
                        MailHelper.SendMail(mensagem);

                        logMensagens.Status = "SUCESSO";
                        logMensagens.Mensagem = $"Email enviado com sucesso para: {mensagem.Destinatario}";

                    }
                    catch (Exception e)
                    {
                        logMensagens.Status = "ERRO";
                        logMensagens.Mensagem = $"Falha ao enviar email para: {mensagem.Destinatario} -> {e.Message}";

                    }
                    finally
                    {
                    
                        var logMensagensPersistence = new LogMensagensPersistence();
                        logMensagensPersistence.Insert(logMensagens);
                    }

                    _model?.BasicAck(args.DeliveryTag, false);
                }

            };

            _model?.BasicConsume(RabbitMQSettings.RabbitQueue, false, consumer);
            return Task.CompletedTask;

        }
    }
}
