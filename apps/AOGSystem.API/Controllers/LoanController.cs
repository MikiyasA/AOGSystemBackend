using AOGSystem.Application.Loans.Command;
using AOGSystem.Domain.Loans;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace AOGSystem.API.Controllers
{
    [ApiController]
    [Route("api/[controller]/[Action]")]
    public class LoanController : Controller
    {

        private readonly IMediator _mediator;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly ILoanRepository _loanRepository;
        private readonly ILoanPartListRepository _loanPartListRepository;
        private readonly IOfferRepository _offerRepository;
        public LoanController(IMediator mediator, IHttpContextAccessor httpContextAccessor, ILoanRepository loanRepository, 
            ILoanPartListRepository loanPartListRepository, IOfferRepository offerRepository)
        {
            _mediator = mediator;
            _httpContextAccessor = httpContextAccessor;
            _loanRepository = loanRepository;
            _loanPartListRepository = loanPartListRepository;
            _offerRepository = offerRepository;
        }

        [HttpPost]
        //[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> CreateLoan([FromBody] CreateLoanComman command)
        {
            try
            {
                //command.SetCreatedBy(User.FindFirst(ClaimTypes.NameIdentifier)?.Value);

                var commandResult = await _mediator.Send(command);

                return commandResult != null ? commandResult.IsSuccess ? Ok(commandResult) : BadRequest(commandResult) : BadRequest();
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }

        [HttpPut]
        //[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        public async Task<IActionResult> UpdateLoan([FromBody] UpdateLoanComman command)
        {
            try
            {
                //command.SetUpdatedBy(User.FindFirst(ClaimTypes.NameIdentifier)?.Value);

                var commandResult = await _mediator.Send(command);

                return commandResult != null ? commandResult.IsSuccess ? Ok(commandResult) : BadRequest(commandResult) : BadRequest();
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }

        [HttpGet]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> GetAllLoans()
        {
            try
            {
                return Ok(await _loanRepository.GetAllLoans());
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }

        [HttpGet("{id}")]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> GetLoanByID(int id)
        {
            try
            {
                return Ok(await _loanRepository.GetLoanByIDAsync(id));
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }

        [HttpGet("{companyId}")]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> GetLoanByCompanyId(int companyId)
        {
            try
            {
                return Ok(await _loanRepository.GetLoanByCompanyIdAsync(companyId));
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }

        [HttpGet("{orderNo}")]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> GetLoanByOrderNoAsync(string orderNo)
        {
            try
            {
                return Ok(await _loanRepository.GetLoanByOrderNoAsync(orderNo));
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }

        [HttpGet("{customerOrderNo}")]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> GetLoanByCustomerOrderNoAsync(string customerOrderNo)
        {
            try
            {
                return Ok(await _loanRepository.GetLoanByCustomerOrderNoAsync(customerOrderNo));
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }

        [HttpGet]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> GetAllActiveLoan()
        {
            try
            {
                return Ok(await _loanRepository.GetAllActiveAsync());
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }

        [HttpPost]
        //[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> LoanApproval([FromBody] LoanApprovalCommand command)
        {
            try
            {
                //command.SetCreatedBy(User.FindFirst(ClaimTypes.NameIdentifier)?.Value);

                var commandResult = await _mediator.Send(command);

                return commandResult != null ? commandResult.IsSuccess ? Ok(commandResult) : BadRequest(commandResult) : BadRequest();
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }

        [HttpPost]
        //[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> LoanCloser([FromBody] LoanCloserCommand command)
        {
            try
            {
                //command.SetCreatedBy(User.FindFirst(ClaimTypes.NameIdentifier)?.Value);

                var commandResult = await _mediator.Send(command);

                return commandResult != null ? commandResult.IsSuccess ? Ok(commandResult) : BadRequest(commandResult) : BadRequest();
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }


        ///////////////////////////// ///

        [HttpPost]
        //[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> AddLoanPartList([FromBody] AddLoanPartListComman command)
        {
            try
            {
                //command.SetCreatedBy(User.FindFirst(ClaimTypes.NameIdentifier)?.Value);

                var commandResult = await _mediator.Send(command);

                return commandResult != null ? commandResult.IsSuccess ? Ok(commandResult) : BadRequest(commandResult) : BadRequest();
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }

        [HttpPut]
        //[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        public async Task<IActionResult> UpdateLoanPartList([FromBody] UpdateLoanPartListCommand command)
        {
            try
            {
                //command.SetUpdatedBy(User.FindFirst(ClaimTypes.NameIdentifier)?.Value);

                var commandResult = await _mediator.Send(command);

                return commandResult != null ? commandResult.IsSuccess ? Ok(commandResult) : BadRequest(commandResult) : BadRequest();
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }

        [HttpGet]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> GetAllLoanPartList()
        {
            try
            {
                return Ok(await _loanPartListRepository.GetAllLoanPartLists());
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }

        [HttpPut]
        //[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        public async Task<IActionResult> LoanPartLineRemoval([FromBody] LoanPartLineRemovalCommand command)
        {
            try
            {
                //command.SetUpdatedBy(User.FindFirst(ClaimTypes.NameIdentifier)?.Value);

                var commandResult = await _mediator.Send(command);

                return commandResult != null ? commandResult.IsSuccess ? Ok(commandResult) : BadRequest(commandResult) : BadRequest();
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }

        [HttpGet("{id}")]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> GetLoanPartListByID(int id)
        {
            try
            {
                return Ok(await _loanPartListRepository.GetLoanPartListByIDAsync(id));
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }


        [HttpPost]
        //[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> ShipLoan([FromBody] ShipLoanPartCommand command)
        {
            try
            {
                //command.SetCreatedBy(User.FindFirst(ClaimTypes.NameIdentifier)?.Value);

                var commandResult = await _mediator.Send(command);

                return commandResult != null ? commandResult.IsSuccess ? Ok(commandResult) : BadRequest(commandResult) : BadRequest();
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }

        


        ///////////////////////////// ///

        [HttpPost]
        //[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> AddOffer([FromBody] AddOfferComman command)
        {
            try
            {
                //command.SetCreatedBy(User.FindFirst(ClaimTypes.NameIdentifier)?.Value);

                var commandResult = await _mediator.Send(command);

                return commandResult != null ? commandResult.IsSuccess ? Ok(commandResult) : BadRequest(commandResult) : BadRequest();
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }

        [HttpPut]
        //[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        public async Task<IActionResult> UpdateOffer([FromBody] UpdateOfferCommand command)
        {
            try
            {
                //command.SetUpdatedBy(User.FindFirst(ClaimTypes.NameIdentifier)?.Value);

                var commandResult = await _mediator.Send(command);

                return commandResult != null ? commandResult.IsSuccess ? Ok(commandResult) : BadRequest(commandResult) : BadRequest();
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }

        [HttpGet]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> GetAllOffers()
        {
            try
            {
                return Ok(await _offerRepository.GetAllOffers());
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }

        [HttpGet("{id}")]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> GetOfferByID(int id)
        {
            try
            {
                return Ok(await _offerRepository.GetOfferByIDAsync(id));
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }


       
    }
}
