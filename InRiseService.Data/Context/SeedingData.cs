using InRiseService.Domain.OrderStatuses;

namespace InRiseService.Data.Context
{
    public static class SeedingData
    {
        public static void Start(ApplicationContext context)
        {
            if(!context.OrderStatuses.Any()){
                var models = new List<OrderStatus>(){
                    new() { Name = "Aguardando Pagamento", IsSendEmail = true, IsVisibleToUser = true },
                    new() { Name = "Pagamento Confirmado", IsSendEmail = true, IsVisibleToUser = true },
                    new() { Name = "Peças Solicitadas", IsSendEmail = false, IsVisibleToUser = false },
                    new() { Name = "Peças Entregues", IsSendEmail = false, IsVisibleToUser = false },
                    new() { Name = "Montagem", IsSendEmail = true, IsVisibleToUser = true },
                    new() { Name = "Distruibuição", IsSendEmail = true, IsVisibleToUser = true },
                    new() { Name = "Concluído", IsSendEmail = true, IsVisibleToUser = true },
                    new() { Name = "Alerta", IsSendEmail = false, IsVisibleToUser = false },
                    new() { Name = "Cancelado", IsSendEmail = false, IsVisibleToUser = false },
                    new() { Name = "Estorno", IsSendEmail = false, IsVisibleToUser = false }
                };
                context.OrderStatuses.AddRange(models);
                context.SaveChanges();
            }

        }
    }
}