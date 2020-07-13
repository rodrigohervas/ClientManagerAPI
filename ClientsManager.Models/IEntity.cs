namespace ClientsManager.Models
{
    /// <summary>
    /// Interface to be implemented by all models
    /// Used to validate Model properties in Validation Filters
    /// </summary>
    public interface IEntity
    {
        public int Id { get; set; }
    }
}