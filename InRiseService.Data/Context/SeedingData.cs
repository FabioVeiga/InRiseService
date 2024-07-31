using InRiseService.Domain.OrderStatuses;
using InRiseService.Domain.Users;

namespace InRiseService.Data.Context
{
    public static class SeedingData
    {
        public static void Start(ApplicationContext context, string psw)
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
            if(!context.Users.Any()){
                var models = new List<User>(){
                    new() { Name = "Administrador", Lastname = "Admin", Email = "admin@inrise.com", Password = psw, Active = true, Term = true, EmailValide = true, InsertIn = DateTime.Now, Profile = Domain.Enums.EnumProfile.Admin},
                    new() { Name = "Fabio", Lastname = "Veiga", Email = "fabinholveiga@gmail.com", Password = psw, Active = true, Term = true, EmailValide = true, InsertIn = DateTime.Now, Profile = Domain.Enums.EnumProfile.User}
                };
                context.Users.AddRange(models);
                context.SaveChanges();
            }
        }
    }
}