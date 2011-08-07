//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;

namespace MapItPrices.Models
{
    public partial class OpenID
    {
        #region Primitive Properties
    
        public virtual string ClaimedIdentifier
        {
            get;
            set;
        }
    
        public virtual int UserID
        {
            get { return _userID; }
            set
            {
                if (_userID != value)
                {
                    if (User != null && User.ID != value)
                    {
                        User = null;
                    }
                    _userID = value;
                }
            }
        }
        private int _userID;
    
        public virtual System.DateTime Created
        {
            get;
            set;
        }

        #endregion
        #region Navigation Properties
    
        public virtual User User
        {
            get { return _user; }
            set
            {
                if (!ReferenceEquals(_user, value))
                {
                    var previousValue = _user;
                    _user = value;
                    FixupUser(previousValue);
                }
            }
        }
        private User _user;

        #endregion
        #region Association Fixup
    
        private void FixupUser(User previousValue)
        {
            if (previousValue != null && previousValue.OpenIDs.Contains(this))
            {
                previousValue.OpenIDs.Remove(this);
            }
    
            if (User != null)
            {
                if (!User.OpenIDs.Contains(this))
                {
                    User.OpenIDs.Add(this);
                }
                if (UserID != User.ID)
                {
                    UserID = User.ID;
                }
            }
        }

        #endregion
    }
}