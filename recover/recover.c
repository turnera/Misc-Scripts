#include <stdio.h>
#include <stdlib.h>
#include <stdbool.h>


int main(int argc, char *argv[])
{
    int i = 0;

    if (argc == 2)
    {
         //open Memory Card
        FILE *file = fopen(argv[1], "r");

        if (file == NULL)
        {
            fprintf(stderr, "Unable to open.\n");

            return 2;
        }

        unsigned char buffer[512];
        char fileName[10];
        FILE *outFile = NULL;


        while (fread(buffer, 512, 1, file) == 1)
        {
            //if bytes == jpeg signature
            bool headerFound = (buffer[0]) == 0xff && (buffer[1]) == 0xd8 && (buffer[2]) == 0xff && ((buffer[3]) & 0xf0) == 0xe0;

             if (headerFound && outFile != NULL)
            {
                fclose(outFile);
                i++;
            }

            if (headerFound)
            {
                sprintf(fileName, "%03i.jpg", i);
                outFile = fopen(fileName, "w");

            }

            if (outFile != NULL)
            {

                fwrite(buffer, 512, 1, outFile);
            }

        }

        fclose(outFile);
        fclose(file);

        return 0;

    }

    else
    {
        printf("Please give 1 argument.\n");

        return 1;
    }

}

//i hate segfault grrrrrrrrrrr