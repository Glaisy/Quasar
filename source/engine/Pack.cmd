if not exist %2 md %2

if exist %2%3.%4 del %2%3.%4
tar -acf %2%3.zip -C %1 *
ren %2%3.zip %3.%4
