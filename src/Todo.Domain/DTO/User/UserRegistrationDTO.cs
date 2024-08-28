namespace Todo.Domain.DTO.User
{
    /// <summary>
    /// Represents a Data Transfer Object (DTO) for an UserRegistrationDTO.
    /// </summary>
    public class UserRegistrationDTO
    {
        /// <summary>
        /// Passwort set by user.
        /// </summary>
        public string Password { get; set; } = null!;

        /// <summary>
        /// Gets or sets the Username.
        /// </summary>
        public string Username { get; set; } = null!;

        /// <summary>1
        /// Gets or sets the FirstName.
        /// </summary>
        public string FirstName { get; set; } = null!;

        /// <summary>
        /// Gets or sets the LastName.
        /// </summary>
        public string LastName { get; set; } = null!;

        /// <summary>
        /// Gets or sets the Email.
        /// </summary>
        public string Email { get; set; } = null!;
    }
}
