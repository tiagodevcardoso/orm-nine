using System.Diagnostics.CodeAnalysis;

namespace ORM.Nine.Database.Models.Attributes
{
    /// <summary>
    ///     Specifies the database column that a property is mapped to.
    /// </summary>
    [AttributeUsage(AttributeTargets.Property, AllowMultiple = false)]
    public class PropertyField : Attribute
    {
        private bool _primary = false;

        private string? _name;

        private string? _type;

        private int _order;

        private bool _valueNull = false;

        private string? _default;

        /// <summary>
        ///     Initializes a new instance of the <see cref="PropertyField" /> class.
        /// </summary>
        public PropertyField()
        {
        }

        /// <summary>
        ///     The database provider specific data type of the column the property is mapped to.
        /// </summary>
        [DisallowNull]
        public bool Primary
        {
            get => _primary;
            set
            {
                _primary = value;
            }
        }

        /// <summary>
        ///     The database provider specific data type of the column the property is mapped to.
        /// </summary>
        [DisallowNull]
        public string? Name
        {
            get => _name;
            set
            {
                _name = value;
            }
        }

        /// <summary>
        ///     The database provider specific data type of the column the property is mapped to.
        /// </summary>
        [DisallowNull]
        public string? Type
        {
            get => _type;
            set
            {
                _type = value;
            }
        }

        /// <summary>
        ///     The database provider specific data type of the column the property is mapped to.
        /// </summary>
        [DisallowNull]
        public int Order
        {
            get => _order;
            set
            {
                _order = value;
            }
        }

        /// <summary>
        ///     The database provider specific data type of the column the property is mapped to.
        /// </summary>
        [DisallowNull]
        public bool ValueNull
        {
            get => _valueNull;
            set
            {
                _valueNull = value;
            }
        }

        /// <summary>
        ///     The database provider specific data type of the column the property is mapped to.
        /// </summary>
        [DisallowNull]
        public string? Default
        {
            get => _default;
            set
            {
                _default = value;
            }
        }
    }
}