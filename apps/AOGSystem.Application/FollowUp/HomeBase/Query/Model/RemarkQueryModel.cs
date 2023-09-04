﻿using AOGSystem.Domain.FollowUp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AOGSystem.Application.FollowUp.HomeBase.Query.Model
{
    public class RemarkSummery
    {
        public int Id { get; set; }
        public int HomeBaseFollowUpId { get; set; }
        public string? Message { get; set; }

        internal static Remark ToModel(RemarkSummery item)
        {
            return new Remark(item.HomeBaseFollowUpId, item.Message);
        }

        internal static List<Remark> ToModel(List<RemarkSummery> lists)
        {
            return lists.Select(x => ToModel(x)).ToList();
        }

    }
    public class RemarkQueryModel : RemarkSummery
    {

    }
}
