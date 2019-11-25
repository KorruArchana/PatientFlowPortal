
if object_id('PolicyTest.Split') is not null
drop function PolicyTest.Split;
go

create function PolicyTest.Split(
  @sText varchar(max), 
  @sDelim varchar(20) = ' ')
returns @retArray table (
  idx smallint primary key,
  value varchar(max)
)
as
/*******************************************************************************
This Table Valued Function can be used in select statements where the list of
paramaters is required from delimited list.

Parameters
  @List  cvs list of parameters.

Example Call
  select * from fn_Split( '320, 20, 99, 20, 33, 99, 777', ',' )

*******************************************************************************/
begin
  declare
    @idx smallint,
	@value varchar(max),
	@bcontinue bit,
	@iStrike smallint,
	@iDelimlength tinyint

  if @sDelim = 'Space'
  begin
	set @sDelim = ' '
  end

  set @idx = 0
  set @sText = ltrim(rtrim(@sText))
  set @iDelimlength = datalength(@sDelim)
  set @bcontinue = 1

  if not ((@iDelimlength = 0) or (@sDelim = 'Empty'))
  begin
	while @bcontinue = 1
    begin
      --If you can find the delimiter in the text, retrieve the first element and
      --insert it with its index into the return table.

      if charindex(@sDelim, @sText)>0
      begin
        set @value = substring(@sText,1, charindex(@sDelim,@sText)-1)
		
        --Trim the element and its delimiter from the front of the string.
        --Increment the index and loop.
        set @iStrike = datalength(@value) + @iDelimlength
        set @sText = ltrim(right(@sText,datalength(@sText) - @iStrike))
      end
      else
      begin
        --If you can’t find the delimiter in the text, @sText is the last value in
        --@retArray.
        set @value = @sText

        --Exit the WHILE loop.
        set @bcontinue = 0
      end

      insert @retArray (idx, value)
      values (@idx, @value)
      set @idx = @idx + 1
    end
  end
  else
  begin
    while @bcontinue=1
    begin
      --If the delimiter is an empty string, check for remaining text
      --instead of a delimiter. Insert the first character into the
      --retArray table. Trim the character from the front of the string.
      --Increment the index and loop.
      if datalength(@sText)>1
      begin
        set @value = SubString(@sText,1,1)
        set @sText = SubString(@sText,2,DataLength(@sText)-1)
      end
      else
      begin
        set @value = @sText
        --One character remains.
        --Insert the character, and exit the WHILE loop.
        set @bcontinue = 0	
      end

      insert @retArray (idx, value)
      values (@idx, @value)			
      set @idx = @idx+1
    end
  end

  return
end
go
