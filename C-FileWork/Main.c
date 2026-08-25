#include <stdio.h>
#include <math.h>
#include <string.h>
#include <windows.h>
#include <time.h>

#define Signature "-_-_-_-BLABLABLATESTSTRINGPRIMITELABUPLS-_-_-_-"

typedef struct City
{
    char *Name;
    float Flat;
    unsigned int People;
} City;

typedef struct FileWithName
{
    char *Name;
    FILE *file;
} FileWithName;

typedef struct List
{
    int size;
    char **stringlist;
} List;

#define Error(test)                                  \
                                                     \
    SetConsoleTextAttribute(handle, FOREGROUND_RED); \
    printf("\n%s\n", test);                          \
    SetConsoleTextAttribute(handle, 7);              \
    fclose(f);                                       \
    Sleep(3000);                                     \
    continue;

#define Success()                                      \
                                                       \
    SetConsoleTextAttribute(handle, FOREGROUND_GREEN); \
    printf("\nSuccess!\n");                            \
    SetConsoleTextAttribute(handle, 7);                \
    Sleep(300);                                        \
    continue;

int SelectMethod(List menu);
char *ReadString(FILE *f);
void UserWrite(FILE *f, int trigger);
FileWithName openfile(char *type, int trigger);
void Sortfile(FILE *f, int temp, int CityCounter, City *CityList, char *Path);
City *Readfile(FILE *f, int *pointer);
void MenuRender(int function, List menu);
char *CreateFilePath(char *name, int AddTxt);
float input(char message[], int bigger);
int check();
List getdirectories();
void PrintSpace(int count, int trigger);
List GetLinesList(City *Array, int count);
void WriteToFile(FILE *f, City TempBook);
int UintInput();

int main()
{
    City *CityList = calloc(1, sizeof(struct City)), TempBook;
    HANDLE handle = GetStdHandle(STD_OUTPUT_HANDLE);
    FILE *f;
    char select, *Name, *Path; //, **Point
    char *array[] = {"Create file", "Read file", "Write in the file", "Delete line", "Delete file", "Edit line", "Sort file", "Insert in sortet file", "Exit"};
    List dirs, templist, menulist;
    menulist.stringlist = array;
    menulist.size = 9;
    int temp, CityCounter = 0;
    FileWithName data;
    do
    {
        CityList = calloc(1, sizeof(struct City));
        switch (SelectMethod(menulist))
        {
        case 0:
            printf("\n\n\nEnter the name of file:\nRools for naming files:\n* Cant consist reserved symbols (\\/.?<>:|\"*),\n* Must be longer than 0 symbols\n* Must be shorter than 100 symbols\n* Can`t be one of reserved names, like \"CON\"\n");
            f = fopen(CreateFilePath("", 1), "w");
            if (!f)
            {
                Error("Cant create this file, try again! Restarting...");
            }
            fprintf(f, "%s0\n", Signature);
            fflush(f);
            fclose(f);
            Success();
            break;

        case 1:
            data = openfile("r", 0);
            f = data.file;
            CityList = Readfile(f, &CityCounter);
            if (CityList == NULL)
            {
                Error("The file is empty or there is an error while reading a file! Restarting...");
            }
            templist = GetLinesList(CityList, CityCounter);
            if (templist.size == 0)
            {
                Error("The file is empty or there is an error while reading a file! Restarting...");
            }
            for (int i = 0; i < templist.size; i++)
            {
                printf("%s\n", templist.stringlist[i]);
            }
            printf("Press any key to go to menu");
            getch();
            fclose(f);
            Success();
            break;

        case 2:
            data = openfile("a", 0);
            f = data.file;
            UserWrite(f, 1);
            fclose(f);
            Success();
            break;

        case 3:
            printf("1");
            data = openfile("r", 0);
            printf("1");
            f = data.file;
            Path = calloc(strlen(data.Name), sizeof(char));
            strcpy(Path, data.Name);
            printf("1");
            CityList = Readfile(f, &CityCounter);
            printf("1");
            templist = GetLinesList(CityList, CityCounter);
            printf("1");
            if (templist.size == 0)
            {
                Error("The file is empty or there is an error while reding a file! Restarting...");
            }
            CityCounter = SelectMethod(templist);
            fclose(f);
            f = fopen(CreateFilePath(Path, 0), "w");
            fprintf(f, "%s0\n", Signature);
            for (int i = 0; i < templist.size; i++)
            {
                if (i != CityCounter)
                {
                    WriteToFile(f, CityList[i]);
                }
            }
            fclose(f);
            Success();
            break;

        case 4:
            dirs = getdirectories();
            CityCounter = SelectMethod(dirs);
            Name = calloc(strlen(dirs.stringlist[CityCounter]), sizeof(char));
            strcpy(Name, dirs.stringlist[CityCounter]);
            printf("Are you sure? Enter \"yes\" to confirm removing this file:");
            Path = ReadString(stdin);
            if (strlen(Path) == 3)
            {
                for (int i = 0; i < strlen(Path); i++)
                {
                    Path[i] = tolower(Path[i]);
                }
                if (!strcmp(Path, "yes"))
                {
                    if (remove(CreateFilePath(Name, 0)))
                    {
                        Error("Cant remove this file! Restarting...");
                    }
                }
            }
            Success();
            break;

        case 5:
            data = openfile("r", 0);
            f = data.file;
            Path = calloc(strlen(data.Name), sizeof(char));
            strcpy(Path, data.Name);

            CityList = Readfile(f, &CityCounter);
            templist = GetLinesList(CityList, CityCounter);
            temp = SelectMethod(templist);
            if (CityList[temp].Name != NULL)
            {
                printf("Old City:\nName: %s\nPopulation: %d\nFlat: %f\n", CityList[temp].Name, CityList[temp].People, CityList[temp].Flat);
                printf("\nLets Edit City\n");
                printf("\nEnter the name of City\n");
                CityList[temp].Name = ReadString(stdin);
                printf("\nEnter the count of peoples in City`\n");
                CityList[temp].People = UintInput();
                CityList[temp].Flat = input("\nEnter the flat of City:", 1);
                fclose(f);
                f = fopen(CreateFilePath(Path, 0), "w");
                fprintf(f, "%s0\n", Signature);
                for (int i = 0; i < CityCounter; i++)
                {
                    WriteToFile(f, CityList[i]);
                }
                fclose(f);
            }
            else
            {
                Error("File is empty!");
            }
            Success();
            break;

        case 6:
            data = openfile("r", 0);
            f = data.file;
            Path = calloc(strlen(data.Name), sizeof(char));
            strcpy(Path, data.Name);

            CityList = Readfile(f, &CityCounter);
            temp = getch();
            temp -= 48;
            while (temp != 1 && temp != 2 && temp != 3)
            {
                temp = getch();
                temp -= 48;
            }
            Sortfile(f, temp, CityCounter, CityList, Path);
            Success();
            break;

        case 7:
            data = openfile("a", 0);
            f = data.file;
            Path = calloc(strlen(data.Name), sizeof(char));
            strcpy(Path, data.Name);
            fseek(f, strlen(Signature), SEEK_SET);
            free(Name);
            Name = calloc(2, sizeof(char));
            fgets(Name, 1, f);
            temp = atoi(Name);
            if (temp != 0)
            {
                Sortfile(f, temp, CityCounter, CityList, Path);
            }
            fclose(f);
            Success();
            break;

        case 8:
            SetConsoleTextAttribute(handle, FOREGROUND_RED);
            printf("Closing...");
            free(Path);
            free(Name);
            free(CityList);
            // dirs, templist, menulist
            if (dirs.size != 0)
            {
                for (int i = 0; i < dirs.size; i++)
                {
                    free(dirs.stringlist[i]);
                }
                free(dirs.stringlist);
            }
            if (templist.size != 0)
            {
                for (int i = 0; i < templist.size; i++)
                {
                    free(templist.stringlist[i]);
                }
                free(templist.stringlist);
            }
            if (menulist.size != 0)
            {
                for (int i = 0; i < menulist.size; i++)
                {
                    free(menulist.stringlist[i]);
                }
                free(menulist.stringlist);
            }
            return 0;
            break;

        default:
            Error("Smth gets wrong :(   Why do You do this? ")
        }
    } while (1);
}

char *CreateFilePath(char *name, int AddTxt)
{
    char *filename = calloc(100, sizeof(char)), *buffer, *pathtofile = "C:\\Users\\makst\\Desktop\\Test9\\";
    if (name[0] == '\000')
    {
        printf("\nEnter the name of file:\t");
        filename = ReadString(stdin);
    }
    else
    {
        filename = name;
    }
    buffer = calloc(strlen(pathtofile) + strlen(filename), sizeof(char));
    if (AddTxt)
    {
        filename = realloc(filename, ((strlen(filename) + strlen(".txt")) * sizeof(char)));
        strncat(filename, ".txt", strlen(".txt"));
    }

    strncat(buffer, pathtofile, strlen(pathtofile));
    strncat(buffer, filename, strlen(filename));
    return buffer;
}
char *ReadString(FILE *f)
{

    HANDLE handle = GetStdHandle(STD_OUTPUT_HANDLE);
    char currentchar = getch(f);
    char *string = calloc(1, sizeof(char));
    string[0] = ' ';
    int counter = 0, i, trigger = 0;
    char *forribenchar = {"\\/.?<>:|\"*\000"};
    char *forribenstring[22] = {"CON", "PRN", "AUX", "NUL", "COM1", "COM2", "COM3", "COM4", "COM5", "COM6", "COM7", "COM8", "COM9", "LPT1", "LPT2", "LPT3", "LPT4", "LPT5", "LPT6", "LPT7", "LPT8", "LPT"};
    while (currentchar != EOF && currentchar != '\n' && currentchar != '\r')
    {
        trigger = 0;
        for (i = 0; i < strlen(forribenchar); i++)
        {
            if (currentchar == forribenchar[i] || (counter >= 100 && currentchar != '\b'))
            {
                SetConsoleTextAttribute(handle, FOREGROUND_RED);
                putchar(currentchar);
                Sleep(400);
                SetConsoleTextAttribute(handle, 7);
                printf("\b \b");
                currentchar = getch(f);
                trigger = 1;
            }
            if (trigger)
            {
                i = 0;
                trigger = 0;
                goto end;
            }
        }
        if (currentchar == '\b')
        {
            if (counter > 0)
            {
                if (counter != 1)
                {
                    putch(currentchar);
                    printf(" \b");
                    string = realloc(string, sizeof(char) * --counter);
                    strncpy(string, string, strlen(string));
                }
                else
                {
                    if (string[0] != '\000')
                    {
                        putch(currentchar);
                        printf(" \b");
                    }
                    string[0] = '\000';
                }
            }
        }
        else
        {
            SetConsoleTextAttribute(handle, FOREGROUND_GREEN);
            string = realloc(string, (strlen(string) * sizeof(char)) + sizeof(char));
            if (string[0] != '\000')
            {
                string[counter] = currentchar;
            }
            else
            {
                string[0] = currentchar;
            }
            putchar(currentchar);
            counter++;
        }
        currentchar = getch(f);
    end:;
    }

    string[counter] = '\000';
    string = realloc(string, strlen(string) + sizeof(char));
    for (i = 0; i < 22; i++)
    {
        if (!strcmp(string, forribenstring[i]))
        {
            SetConsoleTextAttribute(handle, FOREGROUND_RED);
            printf("this name is forriben,try again!\n");
            string = ReadString(f);
            SetConsoleTextAttribute(handle, 7);
        }
    }
    if (counter <= 0)
    {
        SetConsoleTextAttribute(handle, FOREGROUND_RED);
        printf("too short name!!!\n");
        string = ReadString(f);
        SetConsoleTextAttribute(handle, 7);
    }
    fflush(stdin);
    SetConsoleTextAttribute(handle, 7);
    return string;
}

void UserWrite(FILE *f, int trigger)
{
    City TempBook;
    int exit;
    do
    {
        printf("\nLets add new City\n");
        printf("\nEnter the name of City\n");
        TempBook.Name = ReadString(stdin);
        printf("\nEnter the count of peoples in City`\n");
        TempBook.People = UintInput();
        TempBook.Flat = input("\nEnter the flat of City:", 1);
        WriteToFile(f, TempBook);
        printf("\n Press any key to continue, press ESC to go to menu\n");
        exit = getch();
        if (exit == 27)
        {
            exit = 0;
        }
    } while (exit);
}

FileWithName openfile(char *type, int trigger)
{
    HANDLE handle = GetStdHandle(STD_OUTPUT_HANDLE);
    List dirs;
    int CityCounter;
    char *Name;
    FILE *f;
    FileWithName returnme;
    do
    {
        dirs = getdirectories();
        CityCounter = SelectMethod(dirs);
        Name = calloc(strlen(dirs.stringlist[CityCounter]), sizeof(char));
        strcpy(Name, dirs.stringlist[CityCounter]);
        f = fopen(CreateFilePath(Name, trigger), type);
        if (!f)
        {
            Error("Cant create or open this file, try again! Restarting...");
        }
        returnme.file = f;
        returnme.Name = calloc(strlen(Name), sizeof(char));
        strcpy(returnme.Name, Name);
        return returnme;
    } while (1);
}

void Sortfile(FILE *f, int temp, int CityCounter, City *CityList, char *Path)
{
    City TempBook;
    int trigger;

    for (int i = 0; i < CityCounter; i++)
    {
        for (int j = 0; j < CityCounter - i - 1; j++)
        {
            trigger = 0;
            switch (temp)
            {
            case 1:
                if (strcmp(CityList[j].Name, CityList[j + 1].Name) > 0)
                    trigger = 1;
                break;
            case 2:
                if (strcmp(CityList[j].Name, CityList[j + 1].Name) < 0)
                    trigger = 1;
                break;
            case 3:
                if (CityList[j].People > CityList[j + 1].People)
                    trigger = 1;
                break;
            case 4:
                if (CityList[j].People < CityList[j + 1].People)
                    trigger = 1;
                break;
            case 5:
                if (CityList[j].Flat > CityList[j + 1].Flat)
                    trigger = 1;
                break;
            case 6:
                if (CityList[j].Flat < CityList[j + 1].Flat)
                    trigger = 1;
                break;
            }

            if (trigger)
            {
                TempBook = CityList[j];
                CityList[j] = CityList[j + 1];
                CityList[j + 1] = TempBook;
            }
        }
    }
    fclose(f);
    remove(CreateFilePath(Path, 0));
    fopen(CreateFilePath(Path, 0), "w");
    fprintf(f, "%s%d\n", Signature, temp);
    for (int i = 0; i < CityCounter; i++)
    {
        WriteToFile(f, CityList[i]);
    }
}

City *Readfile(FILE *f, int *pointer)
{
    City *CityList = calloc(1, sizeof(City));
    char *Name = "";
    int People = 0, temp = 0;
    float Flat = 0;
    int CityCounter = 0;
    fseek(f, strlen(Signature) + 1, SEEK_SET);
    do
    {
        do
        {
            Name = calloc(100, sizeof(char));
            fseek(f, strlen("City: "), SEEK_CUR);
            if (!fgets(Name, 100, f))
            {
                return CityList;
            }
        } while (!strcmp(Name, "\n"));
        CityList = realloc(CityList, (CityCounter + 2) * sizeof(City));
        fseek(f, strlen("Population: "), SEEK_CUR);
        fscanf(f, "%d", &People);
        fseek(f, strlen("Flat:   ") + 1, SEEK_CUR);
        fscanf(f, "%f", &Flat);
        fseek(f, 2, SEEK_CUR);
        Name = realloc(Name, (strlen(Name) - 1) * sizeof(char));
        Name[strlen(Name) - 1] = '\000';
        if (CityCounter == 0)
        {
            Name[0] = ' ';
        }
        CityList[CityCounter].Name = calloc(strlen(Name), sizeof(char));
        strcpy(CityList[CityCounter].Name, Name);
        CityList[CityCounter].Flat = Flat;
        CityList[CityCounter].People = People;
        temp = fgets(Name, 2, f);
        CityCounter++;
        *pointer = CityCounter;
    } while (temp);

    return CityList;
}

void MenuRender(int function, List menu)
{
    system("cls");
    printf("\nUse ARROW keys to select, press ENTER to sumbit:\n\n");
    for (int counter = 0; counter < menu.size; counter++)
    {
        printf("%s", menu.stringlist[counter]);
        if (counter == function)
        {
            printf("\t%c", 0x16fffffffe);
        }
        putchar('\n');
    }
}

int SelectMethod(List menu)
{
    int selected = 0, trigger = 1;
    do
    {
        MenuRender(selected, menu);
        int control1 = 1, control2 = control1;
        while ((control1 != 224 || (control2 != 80 && control2 != 72)) && control1 != 13)
        {
            control1 = getch();
            if (control1 == 224)
            {
                control2 = getch();
            }
        }
        if (control1 == 13)
        {
            return selected;
        }
        else
        {
            if (control2 == 80)
            {
                if (selected < menu.size - 1)
                {
                    selected++;
                }
            }
            else
            {
                if (selected > 0)
                {
                    selected--;
                }
            }
        }

    } while (trigger);
}

int UintInput()
{
    HANDLE handle = GetStdHandle(STD_OUTPUT_HANDLE);
    SetConsoleTextAttribute(handle, FOREGROUND_GREEN);
    char CurrentChar = 'd';
    char *string = calloc(1, sizeof(char));
    string[0] = '\000';
    int size = 0;
    while (CurrentChar != '\r')
    {
        CurrentChar = getch();
        if (CurrentChar == '\b')
        {
            printf("\b \b");
            string = realloc(string, sizeof(char) * (--size + 1));
        }
        else if (CurrentChar == '\r' || CurrentChar == '\n')
        {
            break;
        }
        else if ((CurrentChar < 48 || CurrentChar > 57))
        {
            SetConsoleTextAttribute(handle, FOREGROUND_RED);
            putch(CurrentChar);
            Sleep(100);
            printf("\b \b");
            SetConsoleTextAttribute(handle, FOREGROUND_GREEN);
        }
        else
        {
            string[size] = CurrentChar;
            string = realloc(string, sizeof(char) * (++size + 1));
            putch(CurrentChar);
        }
    }
    int f = atoi(string);
    SetConsoleTextAttribute(handle, 7);

    free(string);

    return f;
}

float input(char message[], int bigger)
{
    HANDLE handle = GetStdHandle(STD_OUTPUT_HANDLE);
    float x;
    SetConsoleTextAttribute(handle, 7);
    printf("%s\n", message);
    SetConsoleTextAttribute(handle, FOREGROUND_GREEN);
    scanf("%f", &x);
    SetConsoleTextAttribute(handle, 7);
    if (check())
    {
        x = input(message, bigger);
    }
    if (bigger)
    {
        if (x < 0)
        {
            SetConsoleTextAttribute(handle, FOREGROUND_RED);
            printf("Input must be greater than 0!\n");
            Sleep(700);
            printf("\b\b\b\b\b\b\b\b\b\b\b\b\b\b\b\b\b\b\b\b\b\b\b\b\b\b\b\b\b\b                              \b\b\b\b\b\b\b\b\b\b\b\b\b\b\b\b\b\b\b\b\b\b\b\b\b\b\b\b\b\b");
            x = input(message, bigger);
        }
    }
    fflush(stdin);
    return x;
}

int check()
{
    HANDLE handle = GetStdHandle(STD_OUTPUT_HANDLE);
    char str[4];
    fgets(str, sizeof(str), stdin);

    if (strlen(str) > 1)
    {
        SetConsoleTextAttribute(handle, FOREGROUND_RED);
        printf("Invalid input! Try again!\n");
        SetConsoleTextAttribute(handle, 7);
        fflush(stdin);
        return 1;
    }
    else
        return 0;
}
List getdirectories()
{
    char *Signaturecheck;
    WIN32_FIND_DATA f;
    char **filelist = calloc(1, sizeof(char *));
    HANDLE h = FindFirstFile("C:\\Users\\makst\\Desktop\\Test9\\*", &f);
    int count = 0;
    FILE *file;
    Signaturecheck = (char *)calloc(strlen(Signature) + 1, sizeof(char));
    if (h != INVALID_HANDLE_VALUE)
    {
        do
        {
            file = fopen(CreateFilePath(f.cFileName, 0), "r");
            Signaturecheck = calloc(70, sizeof(char));
            fgets(Signaturecheck, strlen(Signature) + 1, file);
            Signaturecheck[strlen(Signaturecheck - 1)] = '\000';
            fclose(file);
            if (!strcmp(Signaturecheck, Signature))
            {
                filelist[count] = (char *)calloc(strlen(f.cFileName), sizeof(char));
                strcpy(filelist[count], f.cFileName);
                filelist = realloc(filelist, (++count + 1) * sizeof(char *));
            }
        } while (FindNextFile(h, &f));
    }
    List result;
    result.stringlist = filelist;
    result.size = count;
    return result;
}

void PrintSpace(int count, int trigger)
{

    for (int i = 0; i < count; i++)
    {
        if (trigger)
        {
            putchar(0);
        }
        else
        {
            putchar(0x16ffffffcd);
        }
    }
}

List GetLinesList(City *Array, int count)
{
    char *string; // = calloc(150, sizeof(char *));
    List returnme;
    returnme.stringlist = calloc(count, sizeof(char *));
    int i = 0;
    for (; i < count;)
    {
        if (Array[i].Name != NULL)
        {
            string = calloc(150, sizeof(char)); //calloc(strlen(Array[i].Name) + 36, sizeof(char));
            sprintf(string, "%s // %d // %f\000", Array[i].Name, Array[i].People, Array[i].Flat);
            string = realloc(string, (strlen(string)) * sizeof(char)); ////
            returnme.stringlist[i] = calloc(strlen(string), sizeof(char));
            strcpy(returnme.stringlist[i], string);
            i++;
        }
    }
    returnme.size = i;
    return returnme;
}

void WriteToFile(FILE *f, City TempBook)
{
    fprintf(f, "City: %s\n", TempBook.Name);
    fprintf(f, "Population: %d\n", TempBook.People);
    fprintf(f, "Flat:   %g\n\n", TempBook.Flat);
    fflush(f);
}
