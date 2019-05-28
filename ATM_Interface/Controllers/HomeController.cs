using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data.Entity;
using ATM_Interface.Models;

namespace ATM_Interface.Controllers
{
    public class HomeController : Controller
    {
        CardContext database = new CardContext();

        Card currentCard = new Card();
        OperationCode operationCode = new OperationCode();
        OperationWithCard operationWithCard = new OperationWithCard();

        public ActionResult CardNumber()
        {
            Session["currentCard"] = null;
            Session["wrongPINCodeCount"] = 0;

            return View();
        }

        [HttpPost]
        public ActionResult CheckCardNumber(string cardNumber)
        {
            try
            {
                currentCard = database.Cards.Where(c => c.CardNumber == cardNumber).First();
            }
            catch (Exception exception)
            {
                ViewBag.ErrorMessage = "Card not found!";
                return View("Error");
            }

            if (currentCard != null)
            {
                if (currentCard.Status == false)
                {
                    ViewBag.ErrorMessage = "Card locked! Unlock your card!";
                    return View("Error");
                }

                Session["currentCard"] = currentCard;
                Session["wrongPINCodeCount"] = 0;

                return View("PINCode");
            }
            else
            {
                ViewBag.ErrorMessage = "Card not found!";
                return View("Error");
            }
        }
        
        public ActionResult PINCode()
        {
            currentCard = (Card)Session["currentCard"];

            return View(currentCard);
        }

        [HttpPost]
        public ActionResult CheckPINCode(string pinCode)
        {
            currentCard = (Card)Session["currentCard"];
            int wrongPINCodeCount = (int)Session["wrongPINCodeCount"];

            if (currentCard != null)
            {
                if (currentCard.PIN == pinCode)
                {
                    return View("CardOperations");
                }
                else
                {
                    wrongPINCodeCount++;

                    if (wrongPINCodeCount < 4)
                    {
                        Session["wrongPINCodeCount"] = wrongPINCodeCount;

                        return View("PINCode");
                    }
                    else
                    {
                        database.Cards.Attach(currentCard);
                        currentCard.Status = false;
                        
                        database.SaveChanges();

                        Session["currentCard"] = currentCard;

                        return View("CardNumber");
                    }
                }
            }
            else
            {
                ViewBag.ErrorMessage = "Card not found!";
                return View("Error");
            }
        }

        public ActionResult Balans()
        {
            Balans balans = new Balans();

            currentCard = (Card)Session["currentCard"];
            
            operationCode = database.OperationCode.Where(c => c.Name == "Balans").First();

            operationWithCard.Id = Guid.NewGuid();
            operationWithCard.OperationCodeId = operationCode.Id;
            operationWithCard.Date = DateTime.UtcNow;

            database.OperationsWithCard.Add(operationWithCard);
            database.SaveChanges();

            balans.CardNumber = currentCard.CardNumber;
            balans.AvailableMoney = currentCard.AvailableMoney;
            balans.Date = DateTime.UtcNow;

            return View(balans);
        }

        public ActionResult CardOperations()
        {
            currentCard = (Card)Session["currentCard"];

            return View(currentCard);
        }

        public ActionResult OperationResult()
        {
            currentCard = (Card)Session["currentCard"];

            return View(currentCard);
        }

        public ActionResult WithdrawMoney()
        {
            currentCard = (Card)Session["currentCard"];

            return View(currentCard);
        }

        [HttpPost]
        public ActionResult WithdrawMoney(decimal withdrawMoney)
        {
            if (withdrawMoney <= 0)
            {
                ViewBag.ErrorMessage = "Incorrect amount of money entered for withdrawal.";
                return View("Error");
            }

            currentCard = (Card)Session["currentCard"];
            OperationType operationType = database.OperationType.Where(op => op.Name == "Withdrawn").First();

            operationCode = database.OperationCode.Where(oc => oc.Name == "WithdrawMoney").First();

            operationWithCard.Id = Guid.NewGuid();
            operationWithCard.OperationCodeId = operationCode.Id;
            operationWithCard.Date = DateTime.UtcNow;

            database.OperationsWithCard.Add(operationWithCard);

            var operation = new Operation();
            var operationResult = new OperationResult();

            if (currentCard.AvailableMoney >= withdrawMoney)
            {
                operation.Id = Guid.NewGuid();
                operation.CardId = currentCard.Id;
                operation.Date = DateTime.UtcNow;
                operation.WithdrawnMoney = withdrawMoney;
                operation.OperationTypeId = operationType.Id;

                database.Cards.Attach(currentCard);
                currentCard.AvailableMoney -= withdrawMoney;

                operationResult.CardNumber = currentCard.CardNumber;
                operationResult.Date = DateTime.UtcNow;
                operationResult.WithdrawnMoney = withdrawMoney;
                operationResult.AvailableMoney = currentCard.AvailableMoney;

                database.Operations.Add(operation);
                database.SaveChanges();
            }
            else
            {
                ViewBag.ErrorMessage = "Not enought money!";
                return View("Error");
            }

            database.SaveChanges();

            return View("OperationResult", operationResult);
        }

        public ActionResult AdminPage()
        {
            ViewBag.Cards = database.Cards;

            return View("AdminPage");
        }

        public ActionResult AddCard()
        {
            return View();
        }

        [HttpPost]
        public ActionResult AddCard(string cardNumber, string pin, decimal availableMoney)
        {
            Card newCard = new Card();

            newCard.Id = Guid.NewGuid();
            newCard.CardNumber = cardNumber;
            newCard.PIN = pin;
            newCard.AvailableMoney = availableMoney;
            newCard.Status = true;

            database.Cards.Add(newCard);
            database.SaveChanges();

            return RedirectToAction("AdminPage");
        }

        //[HttpGet]
        public ActionResult EditCard(Guid id)
        {
            Card card = database.Cards.Where(c => c.Id == id).First();

            return View("EditCard", card);
        }

        [HttpPost]
        public ActionResult EditCard(Guid id, string pin, decimal availableMoney, bool status)
        {
            Card card = database.Cards.Where(c => c.Id == id).First();

            database.Cards.Attach(card);

            card.PIN = pin;
            card.AvailableMoney = availableMoney;
            card.Status = status;

            database.SaveChanges();

            return RedirectToAction("AdminPage");
        }

        [HttpPost]
        public ActionResult DeleteCard(Guid id)
        {
            Card card = database.Cards.Where(c => c.Id == id).First();

            database.Cards.Remove(card);
            database.SaveChanges();

            return RedirectToAction("AdminPage");
        }
    }
}
