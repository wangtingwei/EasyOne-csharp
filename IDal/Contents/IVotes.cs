namespace EasyOne.IDal.Contents
{
    using EasyOne.Model;
    using System;

    public interface IVotes
    {
        void Add(VoteInfo voteInfo);
        void Delete(string generalId);
        VoteInfo GetVoteInfoByGeneralId(int generalId);
        void Update(VoteInfo voteInfo);
    }
}

