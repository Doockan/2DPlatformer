using UnityEngine;

namespace Game.Utils
{
    public class ContactPoller
    {
        private const float _collitionThresh = 0.5f;

        private ContactPoint2D[] _contacts = new ContactPoint2D[10];
        private int _contactsCount;
        private readonly Collider2D _collider2D;
        
        public bool IsGrounded { get; private set; }
        public bool HasLeftContacts { get; private set; }
        public bool HasRightContacts { get; private set; }

        public ContactPoller(Collider2D collider2D)
        {
            _collider2D = collider2D;
        }

        public void Update()
        {
            IsGrounded = false;
            HasLeftContacts = false;
            HasRightContacts = false;

            _contactsCount = _collider2D.GetContacts(_contacts);

            for (int i = 0; i < _contactsCount; i++)
            {
                var normal = _contacts[i].normal;
                var rigidBody = _contacts[i].rigidbody;

                if (normal.y > _collitionThresh) IsGrounded = true;
                if (normal.x > _collitionThresh && rigidBody == null) HasLeftContacts = true;
                if (normal.x < -_collitionThresh && rigidBody == null) HasRightContacts = true;
            }
        }
    }
}