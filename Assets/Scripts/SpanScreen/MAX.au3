#include <misc.au3>
#include <Array.au3>
HotKeySet('{ESC}', '_Exit')

Global $WinText, $OldMouse[2], $NewMouse[2], $Windows, $x, $MyWin, $MyCoords

$NewMouse = MouseGetPos()
$title = _GetWin()
WinSetState($MyWin,"",@SW_RESTORE)
WinMove($MyWin,"",0,0,2560,768)

Func _GetWin()
    Local $Coords
    ToolTip("")
    $Mouse = MouseGetPos()
    $OldMouse = $Mouse
    $Windows = _WinList()
    ;_ArrayDisplay($Windows, "")
    For $x = 1 To UBound($Windows)-1
        $Coords = WinGetPos($Windows[$x][0], "")
        If $Coords = -4 Then ExitLoop
        If IsArray($Coords) Then
            If $Mouse[0] >= $Coords[0] And $Mouse[0] <= ($Coords[0]+$Coords[2]) And $Mouse[1] >= $Coords[1] And $Mouse[1] <= ($Coords[1]+$Coords[3]) Then ExitLoop
        EndIf   
    Next
    If $x = UBound($Windows) Then $x -= 1
    $MyWin =  $Windows[$x][0]
    $Control = _MouseGetCtrlInfo()
    $Return = $Windows[$x][0] & @CRLF & $Control 
    Return $Return
EndFunc 

Func _WinList()
    Local $WinListArray[1][2]
    $var = WinList()
    For $i = 1 to $var[0][0]
        If $var[$i][0] <> "" AND IsVisible($var[$i][1]) Then
            Redim $WinListArray[UBound($WinListArray) + 1][2]
            $WinListArray[UBound($WinListArray)-1][0] = $var[$i][0]
            $WinListArray[UBound($WinListArray)-1][1] = $var[$i][1]
        EndIf
    Next
    Return $WinListArray
EndFunc

Func IsVisible($handle)
  If BitAnd( WinGetState($handle), 2 ) Then 
    Return 1
  Else
    Return 0
  EndIf
EndFunc

Func _Exit()
    Exit
EndFunc 

Func _MouseGetCtrlInfo()  ; get ID, Classe and Text of a control
    Global $hWin = WinGetHandle($MyWin)
    Global $sClassList = WinGetClassList($hWin)
    Local $sSplitClass = StringSplit(StringTrimRight($sClassList, 1), @LF)
    Local $aMPos = MouseGetPos()
    ;_ArrayDisplay($sSplitClass, "")
    $MyCoords = ClientToScreen($hWin)
    For $iCount = UBound($sSplitClass) - 1 To 1 Step - 1
        Local $nCount = 0
        If $sSplitClass[$iCount] = "WorkerW" Then ContinueLoop
        While 1
            $nCount += 1
            $aCPos = ControlGetPos($hWin, '', $sSplitClass[$iCount] & $nCount)
            If @error Then ExitLoop
            $hCtrlWnd = ControlGetHandle ($hWin, "", $sSplitClass[$iCount] & $nCount)
            If IsArray($aCPos) Then
                If $aMPos[0] >= ($MyCoords[0]+$aCPos[0]) And $aMPos[0] <= ($MyCoords[0]+$aCPos[0] + $aCPos[2]) _
                    And $aMPos[1] >= ($MyCoords[1]+$aCPos[1]) And $aMPos[1] <= ($MyCoords[1]+$aCPos[1] + $aCPos[3]) Then
                    $aReturn = DllCall('User32.dll', 'int', 'GetDlgCtrlID', 'hwnd', $hCtrlWnd)
                    If @error Then Return "Err"
                    $Text = ControlGetText($hWin, '', $sSplitClass[$iCount] & $nCount)
                    If StringInStr($Text, @LF) Then $Text = "demasiado largo"
                    If IsArray($aReturn) Then Return 'ControlID: ' & $aReturn[0] & @CRLF & 'ClassNameNN: ' & $sSplitClass[$iCount] & $nCount &  @CRLF & "Text: " & $Text
                EndIf      
            EndIf
        WEnd
    Next
    ;_ArrayDisplay($sSplitClass, "")
    Return "No Ctrl"
EndFunc

Func ClientToScreen($hWnd)    ; get client area of a win relative to the screan
    Local $Point, $aRes[2]
    Local $cX, $cY
    $Point = DllStructCreate("int;int")
    DllStructSetData($Point, 1, $cX)
    DllStructSetData($Point, 1, $cY)
    DllCall("User32.dll", "int", "ClientToScreen", "hwnd", $hWnd, "ptr", DllStructGetPtr($Point))
    $aRes[0] = DllStructGetData($Point, 1)
    $aRes[1] = DllStructGetData($Point, 2)
    Return $aRes
EndFunc