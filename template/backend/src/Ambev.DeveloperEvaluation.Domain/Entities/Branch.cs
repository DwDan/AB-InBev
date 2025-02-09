using Ambev.DeveloperEvaluation.Domain.Common;

namespace Ambev.DeveloperEvaluation.Domain.Entities
{
    public class Branch : BaseEntity
    {
        /// <summary>
        /// Gets the branch's full name.
        /// Must not be null or empty and should contain both first and last names.
        /// </summary>
        public string Name { get; set; } = string.Empty;
    }
}
