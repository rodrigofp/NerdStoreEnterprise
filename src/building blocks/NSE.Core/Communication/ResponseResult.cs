﻿using System.Collections.Generic;

namespace NSE.Core.Communication
{
	public class ResponseResult
	{
		public ResponseResult()
		{
			Errors = new ResponseErrorMessages();
		}

		public string Title { get; set; }
		public int Status { get; set; }
		public ResponseErrorMessages Errors { get; set; }
	}

	public class ResponseErrorMessages
	{
		public List<string> Mensagens { get; set; } = new List<string>();
	}
}
