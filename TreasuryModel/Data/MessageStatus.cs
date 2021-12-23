using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TreasuryModel.Data
{
    public enum MessageStatus
    {
        WAITING_TO_SEND, //First Step
        CHECKING_SANCTION, //Second Step
        FAIL_TO_SEND, //IF Could not Send
        WAITING_TO_REVIEW, //If waiting for feedback 
        APPROVE, 
        DISAPPROVE,
	    TRANSFER_TO_SWIFT
    }
}
