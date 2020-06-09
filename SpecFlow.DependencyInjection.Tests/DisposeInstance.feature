Feature: DisposeInstance

The Dispose method of a binding exposing the IDisposable interface should be called

Scenario Outline: Disposing
The examples can be executed at random so we can't assume the order.
	Then Dispose should have been called the right number of times

    Examples: 
    | times |
    | 0     |
    | 1     |
    | 2     |

