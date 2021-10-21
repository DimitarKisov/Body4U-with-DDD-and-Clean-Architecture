namespace Body4U.Application.Common
{
    public class GlobalConstants
    {
        public class System
        {
            public const string SystemName = "Body4U";
            public const string AdministratorRoleName = "Administrator";
            public const string UserRoleName = "User";
            public const string TrainerRoleName = "Trainer";
            public const string Wrong = "Something went wrong in {0}.";
            public const string Desc = "desc";
            public const string Asc = "asc";
            public const string FileIsEmpty = "File is empty.";
            public const string WrongRights = "You don't have right for this action.";
        }

        public class Account
        {
            public const string WrongImageFormat = "Please choose a picture with format .jpg/.jpeg or .png";
            public const string EmptyFile = "The file is empty.";
            public const string EmailExists = "Вече съществува потребител с такъв email.";
            public const string PleaseConfirmEmail = "Моля потвърдете вашия email.";
            public const string InvalidPasswordResetToken = "Невалиден email или reset token.";
            public const string UnssuccesfulPasswordReset = "Неуспешно подновяване";
            public const string RegistrationUnssuccesful = "Sorry, your registration attempt was unsuccessful";
            public const string WrongRights = "Нямате права за това действие.";
            public const string WrongUsernameOrPassword = "Wrong email or password.";
            public const string Locked = "Account is locked.";
            public const string Unauthorized = "Unauthorized";
            public const string PasswordChangeFail = "Unssuccesful password change.";
            public const string LoginFailed = "Login failed.";
            public const string WrongId = "User with this Id: '{0}' was not found.";
            public const string EmailProblem = "Email was not sent successfuly.";
            public const string UnssuccesfulEmailConfirmation = "Email activation was not successful.";
            public const string EmailNotConfirmed = "Email is not confirmed.";
            public const string WrongGender = "There is no such gender.";
            public const string EmailConfirmSubject = "Email Confirmation";
            public const string EmailConfirmHtmlContent = "<p>To confirm your email, please click <a href=\"{0}\">HERE</a></p>";
            public const string ForgotPasswordSubject = "Password Reset";
            public const string ForgotPasswordHtmlContent = "<p>To reset your password, please click <a href=\"{0}\">HERE</a></p>";
        }

        public class Trainer
        {
            public const string TrainerVideoUrl = "Имате зададен грешен линк за видео.";
            public const string MaxTrainerImages = "Не може да имате повече от 6 снимки.";
            public const int MaxTrainerImagesCount = 6;
            public const int MaxTrainerVideosCount = 3;
            public const string WrongId = "Trainer with this Id: '{0}' was not found.";
            public const string NotTrainer = "This user is not a trainer.";
            public const string InfoMissing = "You have incomplete trainer profile information.";
        }

        public class Article
        {
            public const string ArticleTitleExsists = "There is already an article with this title.";
            public const string NotReadyToWriteArticle = "Моля попълнете нужната информация за Вас, за да можете, да създадете статия.";
            public const string WrongImageWidthHeight = "Моля въведете снимка с размери минимум 730x548.";
            public const string ArticleMissing = "There is no such article.";
            public const string WrongImageFormat = "Wrong image format. Choose a file format '.jpg', '.jpeg' or '.png'";
            public const string WrongArticleType = "Wrong article type.";
        }

        public class Common
        {
            public const string NotFound = "Търсената от Вас страница не може да бъде намерена.";
            public const string Wrong = "Нещо се обърка. Моля опитайте пак!";
            public const string WrongId = "Грешен идентификационен номер.";
        }
    }
}
