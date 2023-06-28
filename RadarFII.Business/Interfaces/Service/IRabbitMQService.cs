using RadarFII.Business.Models;

namespace RadarFII.Business.Interfaces.Service
{
    public interface IRabbitMQService
    {
        void Publicar(ProventoFII proventoFII);
    }
}
