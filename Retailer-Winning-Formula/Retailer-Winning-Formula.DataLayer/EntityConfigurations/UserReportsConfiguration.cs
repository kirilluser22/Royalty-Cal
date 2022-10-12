//using Microsoft.EntityFrameworkCore;
//using Microsoft.EntityFrameworkCore.Metadata.Builders;
//using Newtonsoft.Json;
//using Retailer_Winning_Formula.Entities;

//namespace Retailer_Winning_Formula.DataLayer.EntityConfigurations
//{
//    public class UserReportsConfiguration : IEntityTypeConfiguration<UserReports>
//    {
//        public void Configure(EntityTypeBuilder<UserReports> builder)
//        {
//            builder.Property(e => e.DefaultFactors).HasConversion(
//            v => JsonConvert.SerializeObject(v, new JsonSerializerSettings { NullValueHandling = NullValueHandling.Ignore }),
//            v => JsonConvert.DeserializeObject<DefaultFactors>(v, new JsonSerializerSettings { NullValueHandling = NullValueHandling.Ignore }));
//        }
//    }
//}
