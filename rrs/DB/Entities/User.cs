namespace rrs.DB.Entities
{
    public class User
    {
        public int UserId { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        private string _passwordHash;

        // Reltions
        //public virtual ICollection<Reservation> Reservations { get; set; }
        public virtual ICollection<UserRole> UserRoles { get; set; }
        // Método para asignar la contraseña
        public void SetPassword(string password)
        {
            _passwordHash = HashPassword(password);
        }

        // Método para verificar la contraseña
        public bool VerifyPassword(string password)
        {
            return VerifyPasswordHash(password, _passwordHash);
        }

        // Aquí incluirás los métodos para hashear y verificar el hash de la contraseña
        private string HashPassword(string password)
        {
            //create password hash
            return password;
            
        }

        private bool VerifyPasswordHash(string password, string storedHash)
        {
            //create hash verification
            return password == storedHash;
        }
    }
}
