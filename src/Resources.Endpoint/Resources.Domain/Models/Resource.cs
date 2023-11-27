using Resources.Endpoint.Resources.Domain.Models.Exceptions;

namespace Resources.Endpoint.Resources.Domain.Models
{
    public class Resource
    {
        public required Guid Id { get; set; }
        public required string Name { get; set; }
        
        /// <summary>
        /// Id użytkownika, który dodał zasób
        /// </summary>
        public Guid CreatorUserId { get; set; }

        // TODO: Do rozważenia wydzielenie propert dot. wycofania zasobu do osobnego bytu
         
        /// <summary>
        /// Flaga wycofania zasobu
        /// </summary>
        public bool Canceled { get; set; }

        /// <summary>
        /// Id użytkownika, który zasób wycofał
        /// </summary>
        public Guid? CancelerUserId { get; set; }

        /// <summary>
        /// Data wycofania zasobu
        /// </summary>
        public DateTime? CancelDate { get; set; }


        public void Cancel(Guid cancelerUserId)
        {
            Canceled = true;
            CancelerUserId = cancelerUserId;
            CancelDate = DateTime.Now;
        }
    }
}
