using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using service_account.Repositories;
using service_account.Repositories.Interfaces;
using service_account.CustomResults;
using service_account.Results;
using service_account.Requests;
using service_account.Entities;
using System.Globalization;
using service_account.Domain;
using Microsoft.AspNetCore.Authorization;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace service_account.Controllers;

[ApiController]
[Route("[controller]")]
[Authorize]
public class AccountController : Controller
{
    private readonly UnitOfWork _unitOfWork;
    private readonly AccountDomain _accountDomain;

    public AccountController(IUnitOfWork unitOfWork, IAccountDomain accountDomain )
    {
        _unitOfWork = (UnitOfWork)unitOfWork;
        _accountDomain = (AccountDomain)accountDomain;
    }

    [HttpGet]
    public async Task<IActionResult> Get()
    {
        try
        {

            var accounts = _unitOfWork.Account.GetAll();
            return new MyOkResult(accounts.Select(x => (AccountResult)x).ToList());
        }
        catch (Exception ex)
        {
            return new ErrorResult(ex.Message);
        }
    }

    [HttpPost]
    public async Task<IActionResult> Post([FromBody] AccountRequest request)
    {
        try
        { 
            var account = await _accountDomain.Create(request.Name);

            return new MyOkResult((AccountResult)account);
        }
        catch (Exception ex)
        {
            return new ErrorResult(ex.Message);
        }

    }


    [HttpGet("{accountId}")]
    public async Task<IActionResult> GetAccount(string accountId)
    {
        try
        {
            var account = _unitOfWork.Account.SingleOrDefault(x => x.AccountID == accountId);
            if (account == null)
            {
                return new ErrorResult("Account not found");

            }

            return new MyOkResult((AccountResult)account);
        }
        catch (Exception ex)
        {
            return new ErrorResult(ex.Message);
        }
      

    }


    [HttpPost("{accountId}/withdrawal")]
    public async Task<IActionResult> Withdrawal(string accountId, [FromBody] TransactionRequest request)
    {
        try
        { 
            var transaction =await _accountDomain.Withdrawal(accountId, request.Value);

            return new MyOkResult((TransactionResult)transaction);

        }
        catch (Exception ex)
        {
            return new ErrorResult(ex.Message);

        }
       

    }


    [HttpPost("{accountId}/deposit")]
    public async Task<IActionResult> Deposit(string accountId, [FromBody] TransactionRequest request)
    {
        try
        {
            var transaction = await _accountDomain.Deposit(accountId, request.Value);

            return new MyOkResult((TransactionResult)transaction);
             

        }
        catch (Exception ex)
        {
            return new ErrorResult(ex.Message);

        }
       
    }

    [HttpGet("{accountId}/statement")]
    public IActionResult Statement(string accountId )
    {
        try
        {
            var account = _unitOfWork.Account.SingleOrDefault(x => x.AccountID == accountId);
            if (account == null)
            {
                return new ErrorResult("Account not found");

            }

            var statements = _unitOfWork.Transaction.Find(x => x.AccountID == accountId);

            return new MyOkResult(statements.Select(x => (TransactionResult)x).ToList());

        }
        catch (Exception ex)
        {
            return new ErrorResult(ex.Message);

        }

         
    }
     

    [HttpGet("{accountId}/report")]
    public IActionResult Report(string accountId, string date)
    {
        try
        {
            var account = _unitOfWork.Account.SingleOrDefault(x => x.AccountID == accountId);
            if (account == null)
            {
                return new ErrorResult("Account not found");

            }

            DateTime dt_report = DateTime.MinValue;
            if (!string.IsNullOrEmpty(date))
            {
                dt_report = DateTime.ParseExact(date, "yyyy-MM-dd", CultureInfo.InvariantCulture);
            }
             
            var statements = _unitOfWork.Transaction.Find(x => x.AccountID == accountId && x.Created.Date == dt_report.Date);

            return new MyOkResult((ReportResult)statements.ToList());

        }
        catch (FormatException ex)
        {
            return new ErrorResult("Invalid date! use the formart yyyy-MM-dd");

        }
        catch (Exception ex)
        {
            return new ErrorResult(ex.Message);

        }


    }

}


