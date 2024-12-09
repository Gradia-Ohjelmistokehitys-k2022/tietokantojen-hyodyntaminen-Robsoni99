use library
select member.FirstName, member.LastName, Book.ISBN from Loan INNER JOIN member on loan.MemberId=Member.MemberId INNER JOIN Book on book.BookId=Loan.BookId;