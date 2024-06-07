using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using System.Threading.Tasks;

namespace UsuariosApp.Infra.Messages.Settings
{
    public class RabbitMQSettings
    {
        public static string RabbiUrl = "amqps://hzbcmdpf:cLu8p0FMXCCUxJ-iGtdGg6xWWCiatZd0@shark.rmq.cloudamqp.com/hzbcmdpf";

        public static string RabbitQueue = "emails_usuario";

    }
}
