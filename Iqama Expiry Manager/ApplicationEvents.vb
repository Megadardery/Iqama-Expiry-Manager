Namespace My

    ' The following events are available for MyApplication:
    ' 
    ' Startup: Raised when the application starts, before the startup form is created.
    ' Shutdown: Raised after all application forms are closed.  This event is not raised if the application terminates abnormally.
    ' UnhandledException: Raised if the application encounters an unhandled exception.
    ' StartupNextInstance: Raised when launching a single-instance application and the application is already active. 
    ' NetworkAvailabilityChanged: Raised when the network connection is connected or disconnected.
    Partial Friend Class MyApplication

        Private Sub MyApplication_UnhandledException(sender As Object, e As ApplicationServices.UnhandledExceptionEventArgs) Handles Me.UnhandledException
            MessageBox.Show("Unfortuently, an unhandled error occured. Please report the error to Megadardery@yahoo.com with the following message:" _
                            & Environment.NewLine & Environment.NewLine & GetExceptionInfo(e.Exception), "Unhandled Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            e.ExitApplication = True

        End Sub
    End Class
End Namespace

