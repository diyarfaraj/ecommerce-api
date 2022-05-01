﻿using System;
using Microsoft.AspNetCore.Mvc;

namespace ecommerceApi.Controllers
{
    public class BuggyController : BaseApiController
    {
        [HttpGet("not-found")]
        public ActionResult GetNotFound()
        {
            return NotFound();
        }

        [HttpGet("bad-request")]
        public ActionResult GetBadRequest()
        {
            return BadRequest(new ProblemDetails { Title= "this is a bad request" });
        }

        [HttpGet("unauthrised")]
        public ActionResult GetUnAuthorised()
        {
            return Unauthorized();
        }

        [HttpGet("validation-error")]
        public ActionResult GetValidationError()
        {
            ModelState.AddModelError("Problem1", "this is the first error");
            ModelState.AddModelError("Problem2", "this is the second error");
            return ValidationProblem();

        }

        [HttpGet("server-error")]
        public ActionResult GetServerError()
        {
            throw new Exception("This is a server error");
        }
    }
}
